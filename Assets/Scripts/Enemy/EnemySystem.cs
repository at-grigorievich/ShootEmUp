using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySystem
    {
        private readonly EnemyPool _pool;
        
        private HashSet<EnemyController> _activeEnemies;

        public int AddEnemyDelay { get; private set; }

        public EnemySystem(EnemyPositions enemyPositions, ITargeteable target, BulletSystem bulletSystem,
            EnemyView enemyInstance, int initialCount, int addEnemyDelay, Transform root)
        {
            _pool = new EnemyPool(enemyPositions, target, bulletSystem, enemyInstance, initialCount, root);
            _activeEnemies = new HashSet<EnemyController>();
            AddEnemyDelay = addEnemyDelay;
        }

        public void FixedUpdate()
        {
            foreach (EnemyController enemy in _activeEnemies)
            {
                enemy.FixedUpdate();
            }
        }

        public void SetActive(bool isActive)
        {
            if (isActive == true)
            {
                if (_pool.IsEmpty == true)
                {
                    _pool.CreatePool();
                }
            }
            else
            {
                foreach (var enemy in _activeEnemies.ToArray())
                {
                    RemoveEnemy(enemy);
                }
            }
        }

        public void AddEnemy()
        {
            EnemyController newEnemy = _pool.Get();
            _activeEnemies.Add(newEnemy);

            newEnemy.OnDestroyed += OnEnemyDestroyed;
        }

        private void RemoveEnemy(EnemyController enemyController)
        {
            if (_activeEnemies.Remove(enemyController) == true)
            {
                _pool.Post(enemyController);
            }
        }

        private void OnEnemyDestroyed(EnemyController enemyController)
        {
            if (_activeEnemies.Contains(enemyController) == false) return;
            RemoveEnemy(enemyController);
            enemyController.OnDestroyed -= OnEnemyDestroyed;
        }
    }
}