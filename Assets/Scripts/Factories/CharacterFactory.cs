using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class CharacterFactory
    {
        [SerializeField] private CharacterView view;

        public CharacterController Create()
        {
            CharacterController characterController = new CharacterController(view);
            return characterController;
        }
    }
}