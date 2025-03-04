using System;
using UnityEngine;
using VContainer;

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

        public void Register(IContainerBuilder builder)
        {
            builder.Register<BulletSystem>(Lifetime.Singleton)
                .WithParameter(bulletInstance)
                .WithParameter(initialBulletCount)
                .WithParameter(bulletsParent)
                .WithParameter(bounds)
                .AsSelf().AsImplementedInterfaces();
        }

    }
}