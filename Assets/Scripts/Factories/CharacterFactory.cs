using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class CharacterFactory
    {
        [SerializeField] private CharacterView view;

        public CharacterController Create(BulletSystem bulletSystem)
        {
            CharacterController characterController = new CharacterController(view, bulletSystem);
            return characterController;
        }
    }
}