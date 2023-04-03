using System.Collections;
using UnityEngine;

namespace Game
{
    public class SphereManager : Manager
    {
        [SerializeField]
        private GameObject spherePrefab;

        [SerializeField]
        private GameObject particlePrefab;

        [SerializeField]
        [Range(0, 300)]
        private float sphereSpeed;

        [SerializeField]
        [Range(1, 100)]
        private float sphereAcceleration;

        [SerializeField]
        [Range(0, 1)]
        private float centripetalSpeed = 0.7f;

        [SerializeField]
        private Color startColor;

        [SerializeField]
        private Color endColor;

        private ParticleSystem _particleSystem;

        private GameObject _sphere;

        private Renderer _sphereRenderer;
        private float _distance;
        private float _radius = 10;

        private Coroutine _moveCoroutine;

        private bool _isPaused = false;

        public float Distance => _distance;

        public override void InitManager(ManagersInitializer managersInitializer)
        {
            base.InitManager(managersInitializer);

            if (_managersInitializer == null) return;
            _managersInitializer.EventManager.SubscribeButtonPressed(KeyCode.Space, StopMoving);
            _managersInitializer.EventManager.GameStart += StartMoving;
            SpawnParticles();
        }

        private IEnumerator MoveSphere()
        {
            if (_sphere == null) yield break;
            while (_radius > 0.01)
            {
                if (_isPaused)
                {
                    yield return new WaitForSeconds(5);

                    _isPaused = false;
                    _managersInitializer.EventManager.OnGameResumed();
                    sphereSpeed = 0;
                }

                if (sphereSpeed < 300)
                {
                    sphereSpeed += Time.deltaTime * sphereAcceleration;
                }

                _distance += Time.deltaTime * sphereSpeed;

                _radius -= Time.deltaTime* centripetalSpeed;

                _sphereRenderer.material.color = Color.Lerp(endColor, startColor, _radius / 10);


                float x = _radius * Mathf.Cos(Mathf.Deg2Rad * _distance);
                float z = _radius * Mathf.Sin(Mathf.Deg2Rad * _distance);
                float y = 0;

                _sphere.transform.position = new Vector3(x, y, z);

                yield return new WaitForEndOfFrame();
            }
            _particleSystem?.Play();
            while (_sphere != null)
            {
                _sphere.transform.localScale = Vector3.Lerp(_sphere.transform.localScale, Vector3.zero, Time.deltaTime);

                if (Vector3.Distance(_sphere.transform.localScale, Vector3.zero) < 0.1)
                {
                    Destroy(_sphere);
                    _managersInitializer.EventManager.UnsubscribeButtonPressed(KeyCode.Space, StopMoving);
                    StopCoroutine(_moveCoroutine);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void SpawnSphere()
        {
            if(spherePrefab == null) return;
            _sphere = Instantiate(spherePrefab, new Vector3(10, 0, 0), Quaternion.identity);
            if (_sphere.TryGetComponent<Renderer>(out var comp))
            {
                _sphereRenderer = comp;
            }
        }

        private void SpawnParticles()
        {
            if (particlePrefab == null) return;
            var particleObject = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
            if (particleObject.TryGetComponent<ParticleSystem>(out var comp))
            {
                _particleSystem = comp;
                _particleSystem.Stop();
            }

        }

        public void StopMoving()
        {
            _isPaused = true;
        }

        public void StartMoving()
        {
            _moveCoroutine = StartCoroutine(MoveSphere());
        }

        void Awake()
        {
            SpawnSphere();
        }

    }
}
