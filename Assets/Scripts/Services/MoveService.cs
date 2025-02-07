using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveableService
    {
        public void Move(Vector2 direction, float speed);
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
    }
}