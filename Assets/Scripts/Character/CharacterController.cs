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

        private readonly InputService _inputService;

        private readonly BulletSystem _bulletSystem;
        
        private readonly IHpEditor _hpEditor;

        public Vector2 Position => _view.transform.position;

        //[SerializeField] private GameManager gameManager;


        public CharacterController(CharacterView view, InputService inputService, BulletSystem bulletSystem)
        {
            _view = view;
            _hpEditor = new HitPointsService(_view.HitPointsComponent);
            _bulletSystem = bulletSystem;
        
            _inputService = inputService;
        }

        public void SetActive(bool isActive)
        {
            if(isActive == true)
            {
                _hpEditor.OnHpEmpty += OnCharacterDeath;
                _inputService.OnFiredClicked += OnFlyBullet;
            }
            else
            {
                _hpEditor.OnHpEmpty -= OnCharacterDeath;
                _inputService.OnFiredClicked -= OnFlyBullet;
            }
        }

        private void OnCharacterDeath()
        {
            //this.gameManager.FinishGame();
        }

        public void Update()
        {
            _view.Move(_inputService.InpuAxis);
        }

        private void OnFlyBullet()
        {
            var weapon = _view.WeaponComponent;
            weapon.Shoot(_bulletSystem, true);
        }
    }
}