using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        private readonly CharacterView _view;

        private readonly IHpEditor _hpEditor;

        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        public CharacterController(CharacterView view)
        {
            _view = view;
            _hpEditor = new HitPointsService(_view.HitPointsComponent);
        }

        public void SetActive(bool isActive)
        {
            if(isActive == true)
            {
                _hpEditor.OnHpEmpty += OnCharacterDeath;
            }
            else
            {
                _hpEditor.OnHpEmpty -= OnCharacterDeath;
            }
        }

        private void OnCharacterDeath()
        {
            this.gameManager.FinishGame();
        }

        private void FixedUpdate()
        {
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            //var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                //position = weapon.Position,
                //velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}