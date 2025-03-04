using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface ITargeteable
    {
        Vector2 Position {get;}
    }

    public sealed class CharacterController: ITargeteable,
        IUserInputListener, IStartGameListener, IPauseGameListener, 
        IUpdateGameListener, IFinishGameListener
    {
        private readonly CharacterView _view;

        private readonly IWeapon _weapon;
        private readonly IHpEditor _hpEditor;
        private readonly IMoveableService _moveService;
        
        private readonly IGameFinalizator _gameFinalizator;

        public Vector2 Position => _view.transform.position;

        public event Action OnFinished
        {
            add => _hpEditor.OnHpEmpty += value;
            remove => _hpEditor.OnHpEmpty -= value;
        }

        public CharacterController(CharacterView view, IWeapon weapon, IHpEditor hpEditor, IMoveableService moveService,
            IGameFinalizator gameFinalizator,
            TeamComponentData teamComponentData)
        {
            _view = view;
            _view.IsPlayer = teamComponentData.IsPlayer;

            _moveService = moveService;
            _hpEditor = hpEditor;
            _weapon = weapon;
            
            _gameFinalizator = gameFinalizator;
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
            
            _hpEditor.ResetHp();
            _weapon.SetActive(true);
            _moveService.SetActive(true);

            _hpEditor.OnHpEmpty += _gameFinalizator.Final;
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
            
            SetVisible(false);
            _view.PlaceDefaultPosition();
            
            _hpEditor.OnHpEmpty -= _gameFinalizator.Final;
            _view.OnDamaged -= _hpEditor.TakeDamage;
        }

        public void SetVisible(bool isVisible) => _view.SetVisible(isVisible);
    }
}