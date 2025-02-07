using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletView : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public bool IsPlayer {get; private set;}
        public int Damage {get; private set;}

        public Vector3 Position => transform.position;

        public event Action<BulletView, Collision2D> OnCollisionEntered;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetActive(bool isActive)
        {
            spriteRenderer.enabled = isActive;
            
            if(isActive == false)
            {
                gameObject.layer = (int)PhysicsLayer.DISABLED;
            }
        }

        public void UpdateData(BulletSystem.BulletDataArgs arg)
        {
            Damage = arg.Damage;
            IsPlayer = arg.IsPlayer;

            SetPosition(arg.Position);
            SetColor(arg.Color);
            SetPhysicsLayer(arg.PhysicsLayer);
            SetVelocity(arg.Velocity);
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}