using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyController: IStartGameListener, IPauseGameListener, IFinishGameListener, 
        IFixedUpdateGameListener
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
        
        public void Start()
        {
            _view.SetActive(true);
            _movement.SetActive(true);
            _weapon.SetActive(true);
            
            _view.OnDamaged += _hpEditor.TakeDamage;
            _movement.OnReachedDestination += OnReachedDestination;

            _hpEditor.OnHpEmpty += OnHpEmptyHandler;
        }

        public void Pause()
        {
            _view.SetLayer(false);
            _movement.SetActive(false);
            _weapon.SetActive(false);
            
            _view.OnDamaged -= _hpEditor.TakeDamage;
            _movement.OnReachedDestination -= OnReachedDestination;

            _hpEditor.OnHpEmpty -= OnHpEmptyHandler;
        }

        public void Resume()
        {
            _view.SetLayer(true);
            _movement.SetActive(true);
            _weapon.SetActive(true);
            
            _view.OnDamaged += _hpEditor.TakeDamage;
            _movement.OnReachedDestination += OnReachedDestination;

            _hpEditor.OnHpEmpty += OnHpEmptyHandler;
        }

        public void Finish()
        {
            _view.SetActive(false);
            _movement.SetActive(false);
            _weapon.SetActive(false);
            
            _view.OnDamaged -= _hpEditor.TakeDamage;
            _movement.OnReachedDestination -= OnReachedDestination;

            _hpEditor.OnHpEmpty -= OnHpEmptyHandler;
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