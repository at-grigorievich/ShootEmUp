using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface ITargeteable
    {
        Vector2 Position {get;}
    }

    public sealed class CharacterController: ITargeteable, IUserInputListener, 
        IStartGameListener, IPauseGameListener, IUpdateGameListener, IFinishGameListener
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

        public CharacterController(CharacterView view, BulletSystem bulletSystem)
        {
            _view = view;

            _moveService = _view.CreateMovement();
            _hpEditor = new HitPointsService(_view.HitPointsComponent);
            _weapon = new InputWeaponService(_view.WeaponComponentData, bulletSystem,true);
        }
        
        public void OnInputUpdated(Vector2 axis)
        {
            _moveService.Move(axis);
        }

        public void OnFireClicked()
        {
            _weapon.Shoot();
        }

        public void Start()
        {
            _view.SetActive(true);
            _weapon.SetActive(true);
            _moveService.SetActive(true);
            
            _view.OnDamaged += _hpEditor.TakeDamage;
        }

        public void Pause()
        {
            _weapon.SetActive(false);
            _moveService.SetActive(false);
            
            _view.OnDamaged -= _hpEditor.TakeDamage;
        }

        public void Resume()
        {
            Start();
        }

        public void Update() { /* added for future */ }

        public void Finish()
        {
            _view.SetActive(false);
            _weapon.SetActive(false);
            _moveService.SetActive(false);
            
            _view.OnDamaged -= _hpEditor.TakeDamage;
        }
    }
}