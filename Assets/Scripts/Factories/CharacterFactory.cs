using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class CharacterFactory
    {
        [SerializeField] private CharacterView view;

        public CharacterController Create(InputService inputService, BulletSystem bulletSystem)
        {
            CharacterController characterController = new CharacterController(view, inputService, bulletSystem);
            return characterController;
        }
    }
}