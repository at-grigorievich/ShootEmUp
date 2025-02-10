using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class EnemySystemFactory
    {
        [SerializeField] private EnemyView enemyInstance;
        [SerializeField] private int needEnemyCount;
        [SerializeField] private int initialEnemyCount;
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform enemiesParent;

        public EnemySystem Create(ITargeteable target, BulletSystem bulletSystem)
        {
            return new EnemySystem(enemyPositions, target, bulletSystem,
                                     enemyInstance, needEnemyCount, initialEnemyCount, enemiesParent);
        }
    }
}