using UnityEngine;

namespace Game
{
    public abstract class Manager : MonoBehaviour
    {
        protected ManagersInitializer _managersInitializer;

        public virtual void InitManager(ManagersInitializer managersInitializer)
        {
            _managersInitializer = managersInitializer;
        }
    }

}
