using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveableService
    {
        void Move(Vector2 direction, float speed);
        void Reset();
    }

    public class MoveByRigidbodyVelocity : IMoveableService
    {
        private readonly Rigidbody2D _rigidbody2D;

        public MoveByRigidbodyVelocity(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void Move(Vector2 direction, float speed)
        {
            var nextPosition = _rigidbody2D.position + direction * speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }

    public class MoveToDestination : IMoveableService
    {
        private readonly Rigidbody2D _rigidbody2D;

        private bool _isReached;

        public MoveToDestination(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void Move(Vector2 destination, float speed)
        {
            if(_isReached == true) return;

            var direction = destination - _rigidbody2D.position;

            if (direction.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            var nextPosition = _rigidbody2D.position + direction.normalized * speed * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void Reset()
        {
            _isReached = false;
        }
    }
}