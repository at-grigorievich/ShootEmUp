using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class BulletSystemFactory
    {
        [SerializeField] private BulletView bulletInstance;
        [SerializeField] private int initialBulletCount;
        [SerializeField] private Transform bulletsParent;
        [Space(10)]
        [SerializeField] private LevelBounds bounds;

        public BulletSystem Create()
        {
            return new BulletSystem(bulletInstance, initialBulletCount, bulletsParent, bounds);
        }

    }
}