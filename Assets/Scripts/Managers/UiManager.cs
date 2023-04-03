
using UnityEngine;

namespace Game
{
    public class UiManager : Manager
    {
        [SerializeField]
        private Canvas startCanvas;
        [SerializeField]
        private Canvas pauseCanvas;

        private StartPopup _startPopup;
        private DistanceMeterPopup _distanceMeterPopup;
        public override void InitManager(ManagersInitializer managersInitializer)
        {
            base.InitManager(managersInitializer);
            if(startCanvas != null)
            {
                if (startCanvas.TryGetComponent<StartPopup>(out var comp))
                {
                    _startPopup = comp;
                    _startPopup.InitPopup(() => StartGame());
                }
                startCanvas.enabled = true;
            }

            if (pauseCanvas != null)
            {
                if (pauseCanvas.TryGetComponent<DistanceMeterPopup>(out var comp))
                {
                    _distanceMeterPopup = comp;
                }
                pauseCanvas.enabled = false;
            }

            if (_managersInitializer == null) return;
            _managersInitializer.EventManager.SubscribeButtonPressed(KeyCode.Space,GamePause);
            _managersInitializer.EventManager.GameResumed += GameResume;
        }

        private void StartGame()
        {
            if (_managersInitializer == null || startCanvas == null) return;
            _managersInitializer.EventManager.OnGameStart();
            startCanvas.enabled = false;
        }

        private void GamePause()
        {
            if (pauseCanvas == null) return;
            _distanceMeterPopup.SetDistanceValue(_managersInitializer.SphereManager.Distance);
            pauseCanvas.enabled = true;
        }

        private void GameResume()
        {
            if (pauseCanvas == null) return;
            pauseCanvas.enabled = false;
        }

    }
}
