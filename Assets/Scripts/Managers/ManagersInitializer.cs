using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ManagersInitializer : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField] 
        private EventManager _eventManager;
        [SerializeField] 
        private SphereManager _sphereManager;

        [SerializeField]
        private UiManager _uiManager;

        public InputManager InputManager => _inputManager;
        public EventManager EventManager => _eventManager;
        public SphereManager SphereManager => _sphereManager;

        public UiManager UiManager => _uiManager;

        void InitManagers()
        {
            if (_inputManager != null)
            {
                _inputManager.InitManager(this);
            }

            if (_eventManager != null)
            {
                _eventManager.InitManager(this);
            }

            if (_sphereManager != null)
            {
                _sphereManager.InitManager(this);
            }

            if (_uiManager != null)
            {
                _uiManager.InitManager(this);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            InitManagers();
        }

    }
}

