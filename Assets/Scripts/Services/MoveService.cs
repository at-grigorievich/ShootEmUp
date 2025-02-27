using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveableService: IActivateable
    {
        void Move();
        void Move(Vector2 direction);
        void Reset();
        void SetDestination(Vector2 destination);

        public event Action OnReachedDestination;
    }

    public class MoveByInput : IMoveableService
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _speed;
        
        public bool IsActive { get; private set; }
        
        public event Action OnReachedDestination;

        public MoveByInput(Rigidbody2D rigidbody2D, float speed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
        
        public void Move(Vector2 direction)
        {
            if(IsActive == false) return;
            
            var nextPosition = _rigidbody2D.position + direction * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
        
        public void Move()
        {
            throw new NotImplementedException("MoveByInput must use Move(Vector2 direction, float speed)");
        }

        public void Reset() => throw new System.NotImplementedException();
        
        public void SetDestination(Vector2 destination) => throw new NotImplementedException();
    }

    public class MoveToDestination : IMoveableService
    {
        private readonly Rigidbody2D _rigidbody2D;

        private readonly float _speed;

        private Vector2 _destination;
        private bool _isReached;

        public bool IsActive { get; private set; }
        
        public event Action OnReachedDestination;

        public MoveToDestination(Rigidbody2D rigidbody2D, float speed)
        {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }
        
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }
        
        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
            Reset();
        }

        public void Move()
        {
            if(_isReached == true) return;

            Move(_destination);
        }

        public void Move(Vector2 destination)
        {
            if(IsActive == false) return;
            
            var direction = destination - _rigidbody2D.position;

            if (direction.magnitude <= 0.25f)
            {
                DestinationReached();
                return;
            }

            var nextPosition = _rigidbody2D.position + direction.normalized * _speed * Time.fixedDeltaTime;
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