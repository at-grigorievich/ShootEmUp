using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface ITargeteable
    {
        Vector2 Position {get;}
    }

    public sealed class CharacterController: ITargeteable
    {
        private readonly CharacterView _view;

        private readonly IWeapon _weapon;
        private readonly IHpEditor _hpEditor;
        private readonly IMoveableService _moveService;

        public Vector2 Position => _view.transform.position;

        public event Action OnDestroyed
        {
            add => _hpEditor.OnHpEmpty += value;
            remove => _hpEditor.OnHpEmpty -= value;
        }

        public CharacterController(CharacterView view, InputService inputService, BulletSystem bulletSystem)
        {
            _view = view;

            _moveService = _view.CreateMovement(inputService);
            _hpEditor = new HitPointsService(_view.HitPointsComponent);
            _weapon = new InputWeaponService(_view.WeaponComponentData, bulletSystem, inputService, true);
        }

        public void SetActive(bool isActive)
        {
            _view.SetActive(isActive);
            _weapon.SetActive(isActive);

            if(isActive == true)
            {
                _view.OnDamaged += _hpEditor.TakeDamage;
            }
            else
            {
                _view.OnDamaged -= _hpEditor.TakeDamage;
            }
        }

        public void Update()
        {
            _moveService.Move();
        }
    }
}