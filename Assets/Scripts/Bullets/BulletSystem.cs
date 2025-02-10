using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem
    {
        private readonly BulletPool _pool;
        private readonly LevelBounds _levelBounds;

        private readonly HashSet<BulletView> _activeBullets;
        
        public BulletSystem(BulletView instance, int initialCount, Transform root, LevelBounds levelBounds)
        {
            _pool = new BulletPool(instance, initialCount, root);
            _activeBullets = new HashSet<BulletView>();
            _levelBounds = levelBounds;
        }

        public void Update()
        {
            HashSet<BulletView> outOfBounds = new HashSet<BulletView>();

            foreach(var bullet in _activeBullets)
            {
                if(_levelBounds.InBounds(bullet.Position) == true) continue;
                outOfBounds.Add(bullet);
            }

            foreach(var outBullet in outOfBounds)
            {
                RemoveBullet(outBullet);
            }
        }

        public void SetActive(bool isActive)
        {
            if(isActive == true)
            {
                if(_pool.IsEmpty == true)
                {
                    _pool.CreatePool();
                }
            }
            else
            {
                foreach(var bullet in _activeBullets)
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletDataArgs args)
        {
            BulletView newBullet = _pool.Get();
            newBullet.UpdateData(args);
            
            if (_activeBullets.Add(newBullet))
            {
                newBullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(BulletView bullet, Collision2D collision)
        {
            Debug.Log("collision");
            //BulletUtils.DealDamage(bullet, collision.gameObject);
            //this.RemoveBullet(bullet);
        }

        private void RemoveBullet(BulletView bullet)
        {
            if(_activeBullets.Remove(bullet) == true)
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _pool.Post(bullet);
            }
        }
        
        public struct BulletDataArgs
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}