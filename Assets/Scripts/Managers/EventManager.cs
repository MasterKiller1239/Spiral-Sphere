using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EventManager : Manager
    {
        private readonly Dictionary<KeyCode, Action> ButtonPressedActions = new Dictionary<KeyCode, Action>();
        public event Action<KeyCode> ButtonPressed;

        public void OnButtonPressed(KeyCode buttonPressed)
        {
            if (ButtonPressedActions.TryGetValue(buttonPressed, out var actions))
            {
                actions?.Invoke();
            }
        }

        public void SubscribeButtonPressed(KeyCode buttonPressed, Action subscription)
        {
            if (ButtonPressedActions.ContainsKey(buttonPressed))
            {
                ButtonPressedActions[buttonPressed] += subscription;
            }
            else
            {
                ButtonPressedActions[buttonPressed] = subscription;
            }
        }

        public void UnsubscribeButtonPressed(KeyCode buttonPressed, Action subscription)
        {
            if (ButtonPressedActions.ContainsKey(buttonPressed))
            {
                ButtonPressedActions[buttonPressed] -= subscription;
            }
            else
            {
                Debug.Log($"There are no Actions binded to {buttonPressed}");
            }
        }

        public event Action GameResumed;

        public void OnGameResumed() => GameResumed?.Invoke();

        public event Action GameStart;

        public void OnGameStart() => GameStart?.Invoke();
    }
}
