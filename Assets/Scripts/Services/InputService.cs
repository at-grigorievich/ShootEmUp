using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputService: IUpdateGameListener
    {
        private readonly IEnumerable<IUserInputListener> _userInputListener;
        
        public InputService(IEnumerable<IUserInputListener> userInputListeners)
        {
            _userInputListener = userInputListeners;
        }
        
        public void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector2 axis = new Vector2(x, y);
            bool onFireClicked = Input.GetKeyDown(KeyCode.Space);
            
            InvokeListeners(axis, onFireClicked);
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