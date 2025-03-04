using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace ShootEmUp
{
    public interface IPauseObserver
    {
        public bool IsPausePressed { get; }
    }
    
    public sealed class InputService: ITickable, IPauseObserver
    {
        private readonly IEnumerable<IUserInputListener> _userInputListener;
        
        public bool IsPausePressed => Input.GetKeyDown(KeyCode.Escape) == true;
        
        public InputService(GameCycleListenersDispatcher gameCycleListenersDispatcher)
        {
            _userInputListener = gameCycleListenersDispatcher.UserInputListeners;
        }
        
        public void Tick()
        {
            Vector2 axis = GetAxis();
            bool onFireClicked = Input.GetKeyDown(KeyCode.Space);
            
            InvokeListeners(axis, onFireClicked);
        }

        private Vector2 GetAxis()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            
            return new Vector2(x, y);
        }
        
        private void InvokeListeners(Vector2 axis, bool onFireClicked)
        {
            foreach (var listener in _userInputListener)
            {
                listener.OnInputUpdated(axis);

                if (onFireClicked == true)
                {
                    listener.OnFireClicked();
                }
            }
        }
    }
}