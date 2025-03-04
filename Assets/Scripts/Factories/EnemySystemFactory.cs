using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemySystemFactory
    {
        [SerializeField] private EnemyView enemyInstance;
        [SerializeField] private int initialEnemyCount;
        [SerializeField] private int addEnemyDelay;
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform enemiesParent;

        public EnemySystem Create(BulletSystem bulletSystem)
        {
            return new EnemySystem(enemyPositions, bulletSystem,
                                     enemyInstance, initialEnemyCount, addEnemyDelay, enemiesParent);
        }

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EnemySpawnService>();
            
            builder.Register<EnemySystem>(Lifetime.Singleton)
                .WithParameter(enemyPositions)
                .WithParameter(enemyInstance)
                .WithParameter("initialCount", initialEnemyCount)
                .WithParameter("addEnemyDelay", addEnemyDelay)
                .WithParameter(enemiesParent)
                .AsSelf().AsImplementedInterfaces();
        }
    }
}