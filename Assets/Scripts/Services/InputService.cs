using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IPauseObserver
    {
        public bool IsPausePressed { get; }
    }

    public sealed class InputPauseObserver : IPauseObserver
    {
        public bool IsPausePressed =>Input.GetKeyDown(KeyCode.Escape) == true;
    }
    
    public sealed class InputService: IUpdateGameListener
    {
        private readonly IEnumerable<IUserInputListener> _userInputListener;
        
        public InputService(IEnumerable<IUserInputListener> userInputListeners)
        {
            _userInputListener = userInputListeners;
        }
        
        public void Update()
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