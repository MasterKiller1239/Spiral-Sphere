using UnityEngine;
namespace Game
{
    public class InputManager : Manager
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _managersInitializer.EventManager.OnButtonPressed(KeyCode.Space);
            }
        }
    }
}
