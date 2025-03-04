using System;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    [Serializable]
    public sealed class LevelBackgroundFactory
    {
        [SerializeField] private LevelBackground.Params parameters;
        [SerializeField] private Transform myTransform;

        public void Register(IContainerBuilder builder)
        {
            builder.Register<LevelBackground>(Lifetime.Singleton)
                .WithParameter(parameters)
                .WithParameter(myTransform)
                .AsImplementedInterfaces();
        }
    }
    
    public sealed class LevelBackground : IFixedUpdateGameListener
    {
        private readonly float _startPositionY;

        private readonly float _endPositionY;

        private readonly float _movingSpeedY;

        private readonly float _positionX;

        private readonly float _positionZ;

        private readonly Transform _myTransform;


        public LevelBackground(Transform myTransform, LevelBackground.Params parameters)
        {
            _startPositionY = parameters.m_startPositionY;
            _endPositionY = parameters.m_endPositionY;
            _movingSpeedY = parameters.m_movingSpeedY;
            _myTransform = myTransform;
            
            var position = _myTransform.position;
            
            _positionX = position.x;
            _positionZ = position.z;
        }
        
        public void FixedUpdate()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }
        

        [Serializable]
        public sealed class Params
        {
            [SerializeField]
            public float m_startPositionY;

            [SerializeField]
            public float m_endPositionY;

            [SerializeField]
            public float m_movingSpeedY;
        }
    }
}