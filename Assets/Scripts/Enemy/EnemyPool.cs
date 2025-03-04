using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{

    public sealed class EnemyPool:GenericPool<EnemyController, EnemyView>
    {
        private readonly EnemyPositions _enemyPositions;

        private readonly BulletSystem _bulletSystem;

        public EnemyPool(EnemyPositions enemyPositions, BulletSystem bulletSystem,
            EnemyView instance, int count, Transform root) 
            : base(instance, count, root)
        {
            _enemyPositions = enemyPositions;
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

            enemyController.Start();

            return enemyController;
        }

        public override void Post(EnemyController element)
        {
            base.Post(element);

            element.SetParent(_root);
            element.Finish();
        }
    }
}