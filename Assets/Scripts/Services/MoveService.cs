using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveableService
    {
        void Move();
        void Move(Vector2 direction, float speed);
        void Reset();
        void SetDestination(Vector2 destination);

        public event Action OnReachedDestination;
    }

    public class MoveByInput : IMoveableService
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly InputService _inputService;
        private readonly float _speed;

        public event Action OnReachedDestination;

        public MoveByInput(Rigidbody2D rigidbody2D, InputService inputService, float speed)
        {
            _rigidbody2D = rigidbody2D;
            _inputService = inputService;
            _speed = speed;
        }

        public void Move()
        {
            Vector2 direction = _inputService.InpuAxis;
            direction.y = 0f;

            Move(direction, _speed);
        }

        public void Reset() => throw new System.NotImplementedException();
        

        public void SetDestination(Vector2 destination) => throw new NotImplementedException();
        
        private void Move(Vector2 direction, float speed)
        {
            var nextPosition = _rigidbody2D.position + direction * speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        void IMoveableService.Move(Vector2 direction, float speed)
        {
            Move(direction, speed);
        }
    }

    public class MoveToDestination : IMoveableService
    {
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _speed;

        private Vector2 _destination;
        private bool _isReached;

        public event Action OnReachedDestination;

        public MoveToDestination(Rigidbody2D rigidbody2D, float speed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }
        
        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
            Reset();
        }

        public void Move()
        {
            if(_isReached == true) return;

            Move(_destination, _speed);
        }

        public void Move(Vector2 destination, float speed)
        {
            var direction = destination - _rigidbody2D.position;

            if (direction.magnitude <= 0.25f)
            {
                DestinationReached();
                return;
            }

            var nextPosition = _rigidbody2D.position + direction.normalized * speed * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void Reset()
        {
            _isReached = false;
        }

        private void DestinationReached()
        {
            _isReached = true;
            OnReachedDestination?.Invoke();
        }
    }
}