using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{

    public sealed class EnemyPool:GenericPool<EnemyController, EnemyView>
    {
        private readonly EnemyPositions _enemyPositions;

        private readonly BulletSystem _bulletSystem;
        private readonly ITargeteable _targeteable;

        public EnemyPool(EnemyPositions enemyPositions, ITargeteable target, BulletSystem bulletSystem,
            EnemyView instance, int count, Transform root) 
            : base(instance, count, root)
        {
            _enemyPositions = enemyPositions;
            _targeteable = target;
            _bulletSystem = bulletSystem;
        }

        protected override EnemyController CreateInstance()
        {
            EnemyView enemyView = GameObject.Instantiate(_instance, _root);
            return new EnemyController(enemyView, _bulletSystem);
        }

        public HashSet<EnemyController> GetMany(int needCount)
        {
            HashSet<EnemyController> _enemies = new HashSet<EnemyController>();
            for(int i = 0; i < needCount; i++)
            {
                _enemies.Add(Get());
            }

            return _enemies;
        }

        public override EnemyController Get()
        {
            EnemyController enemyController = base.Get();
            
            enemyController.SetParent(null);

            var position = _enemyPositions.RandomSpawnPosition().position;

            enemyController.SetPosition(position);
            enemyController.SetDestination(_enemyPositions.RandomAttackPosition().position);

            enemyController.SetTarget(_targeteable);

            enemyController.SetActive(true);

            return enemyController;
        }

        public override void Post(EnemyController element)
        {
            base.Post(element);

            element.SetParent(_root);
            element.SetActive(false);
        }
    }
}