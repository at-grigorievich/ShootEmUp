using System;
using UnityEngine;

namespace ShootEmUp
{
    //TODO: Replace to new input system in the future............
    public sealed class InputService
    {
        public Vector2 InputAxis {get; private set;}
        public event Action OnFiredClicked;

        public void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            InputAxis = new Vector2(x, y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFiredClicked?.Invoke();
            }
        }
    }
}