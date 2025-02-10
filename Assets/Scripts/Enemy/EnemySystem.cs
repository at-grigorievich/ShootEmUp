using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySystem
    {
        private readonly EnemyPool _pool;
        private readonly BulletSystem _bulletSystem;

        private readonly int _needCount;

        private HashSet<EnemyController> _activeEnemies;

        public EnemySystem(EnemyPositions enemyPositions, ITargeteable target, BulletSystem bulletSystem,
            EnemyView enemyInstance,int needCount, int initialCount, Transform root)
        {
            _pool = new EnemyPool(enemyPositions, target, bulletSystem, enemyInstance, initialCount, root);
            _activeEnemies = new HashSet<EnemyController>();
            _needCount = needCount;
        }

        public void FixedUpdate()
        {
            foreach(EnemyController enemy in _activeEnemies)
            {
                enemy.FixedUpdate();
            }
        }

        /*private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._pool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        //enemy.GetComponent<HitPointsComponent>().OnHpEmpty += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }
                }
            }
        }*/

        public void SetActive(bool isActive)
        {
            if (isActive == true)
            {
                if (_pool.IsEmpty == true)
                {
                    _pool.CreatePool();
                }

                _activeEnemies = _pool.GetMany(_needCount);
            }
            else
            {
                foreach (var enemy in _activeEnemies)
                {
                    RemoveEnemy(enemy);
                }
            }
        }

        private void RemoveEnemy(EnemyController enemyController)
        {
            if(_activeEnemies.Remove(enemyController) == true)
            {
                _pool.Post(enemyController);
            }
        }

        /*private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                //enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _pool.UnspawnEnemy(enemy);
            }
        }*/

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.BulletDataArgs
            {
                IsPlayer = false,
                PhysicsLayer = (int)PhysicsLayer.ENEMY,
                Color = Color.red,
                Damage = 1,
                Position = position,
                Velocity = direction * 2.0f
            });
        }
    }
}