using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class WeaponComponentData
    {
        [SerializeField] private Transform weaponTransform;
        [SerializeField] private BulletConfig weaponBulletConfig;

        public WeaponComponent Create()
        {
            return new WeaponComponent(weaponTransform, weaponBulletConfig);
        }
    }

    public sealed class WeaponComponent
    {
        private readonly Transform _weaponTransform;
        private readonly BulletConfig _bulletConfig;

        public Vector2 Position => _weaponTransform.position;
        public Quaternion Rotation => _weaponTransform.rotation;

        public WeaponComponent(Transform weaponTransform, BulletConfig weaponBulletConfig)
        {
            _weaponTransform = weaponTransform;
            _bulletConfig = weaponBulletConfig;
        }

        public void Shoot(BulletSystem bulletSystem, bool isPlayer)
        {
            Shoot(bulletSystem, Position, Rotation * Vector3.up * _bulletConfig.speed, isPlayer);
        }

        public void Shoot(BulletSystem bulletSystem, Vector2 spawnPosition, Vector2 velocity, bool isPlayer)
        {
            bulletSystem.FlyBulletByArgs(new BulletSystem.BulletDataArgs
            {
                IsPlayer = isPlayer,
                PhysicsLayer = (int)_bulletConfig.physicsLayer,
                Color = _bulletConfig.color,
                Damage = _bulletConfig.damage,
                Position = spawnPosition,
                Velocity = velocity
            });
        }
    }
}