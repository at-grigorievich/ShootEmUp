using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem: IStartGameListener, IPauseGameListener, IFinishGameListener,
        IUpdateGameListener
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

        public void Start()
        {
            if(_pool.IsEmpty == true)
            {
                _pool.CreatePool();
            }
            Finish();
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
        
        public void Pause()
        {
            foreach (var bullet in _activeBullets)
            {
                bullet.SetSimulatedStatus(false);
            }
        }

        public void Resume()
        {
            foreach (var bullet in _activeBullets)
            {
                bullet.SetSimulatedStatus(true);
            }
        }

        public void Finish()
        {
            foreach(var bullet in _activeBullets.ToArray())
            {
                RemoveBullet(bullet);
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
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
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