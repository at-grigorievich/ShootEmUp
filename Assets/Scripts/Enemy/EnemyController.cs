using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyController
    {
        private readonly EnemyView _view;

        private readonly IWeapon _weapon;
        
        private readonly IHpEditor _hpEditor;

        private readonly IMoveableService _movement;

        public event Action<EnemyController> OnDestroyed;

        public EnemyController(EnemyView view, BulletSystem bulletSystem)
        {
            _view = view;

            _movement = _view.CreateMovement();
            _weapon = new DelayWeaponService(_view.WeaponComponentData, bulletSystem, 2f, _view.IsPlayer);
            _hpEditor = new HitPointsService(_view.HitPointsComponent);
        }

        public void SetActive(bool isActive)
        {
            _view.SetActive(isActive);
            
            if(isActive == true)
            {
                _view.OnDamaged += _hpEditor.TakeDamage;
                _movement.OnReachedDestination += OnReachedDestination;

                _hpEditor.OnHpEmpty += OnHpEmptyHandler;
            }
            else
            {
                _view.OnDamaged -= _hpEditor.TakeDamage;
                _movement.OnReachedDestination -= OnReachedDestination;

                _hpEditor.OnHpEmpty -= OnHpEmptyHandler;
            }
        }

        public void FixedUpdate()
        {
            _movement.Move();
            _weapon.Update();
        }

        public void SetParent(Transform parent)
        {
            _view.SetParent(parent);
        }

        public void SetPosition(Vector3 position)
        {
            _view.Place(position);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _movement.SetDestination(endPoint);
        }

        public void SetTarget(ITargeteable target)
        {
            _weapon.SetTarget(target);
        }

        private void OnReachedDestination()
        {
            _weapon.SetActive(true);
        }

        private void OnHpEmptyHandler()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}