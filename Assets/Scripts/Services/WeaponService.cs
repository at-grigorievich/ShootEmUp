using UnityEngine;

namespace ShootEmUp
{
    public interface IWeapon
    {
        void SetActive(bool isActive);
        void Shoot();
        void Update();
        void SetTarget(ITargeteable target);
    }

    public sealed class InputWeaponService : IWeapon
    {
        private readonly WeaponComponent _weaponComponent;
        private readonly BulletSystem _bulletSystem;
        private readonly InputService _inputService;

        private readonly bool _isPlayer;

        public InputWeaponService(WeaponComponentData weaponComponent, BulletSystem bulletSystem,
            InputService inputService, bool isPlayer)
        {
            _weaponComponent = weaponComponent.Create();
            _bulletSystem = bulletSystem;
            _inputService = inputService;
            _isPlayer = isPlayer;
        }

        public void SetActive(bool isActive)
        {
            if (isActive == true)
            {
                _inputService.OnFiredClicked += Shoot;
            }
            else
            {
                _inputService.OnFiredClicked -= Shoot;
            }
        }

        public void SetTarget(ITargeteable target)
        {
            throw new System.NotImplementedException();
        }

        public void Shoot()
        {
            _weaponComponent.Shoot(_bulletSystem, _isPlayer);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed class DelayWeaponService : IWeapon
    {
        private readonly WeaponComponent _weaponComponent;
        private readonly BulletSystem _bulletSystem;

        private ITargeteable _target;

        private readonly bool _isPlayer;

        private readonly float _delay;

        private float _currentTimer;

        private bool _isActive;

        public DelayWeaponService(WeaponComponentData weaponComponentData, BulletSystem bulletSystem, 
            float delay, bool isPlayer)
        {
            _weaponComponent = weaponComponentData.Create();
            _bulletSystem = bulletSystem;

            _delay = delay;
            _isPlayer = isPlayer;
        }

        public void SetActive(bool isActive)
        {
            _currentTimer = 0f;
            _isActive = isActive;

            if (isActive == false)
            {
                _target = null;
            }
        }

        public void SetTarget(ITargeteable target)
        {
            _target = target;
        }

        public void Shoot()
        {
            if (_target == null || _isActive == false) return;

            _currentTimer = 0f;

            var startPosition = _weaponComponent.Position;
            var vector = _target.Position- startPosition;
            var direction = vector.normalized;

            _weaponComponent.Shoot(_bulletSystem, startPosition, direction * 2f, _isPlayer);
        }

        public void Update()
        {
            if (_isActive == false) return;

            if (_currentTimer <= _delay)
            {
                _currentTimer += Time.deltaTime;
                return;
            }

            Shoot();
        }
    }
}