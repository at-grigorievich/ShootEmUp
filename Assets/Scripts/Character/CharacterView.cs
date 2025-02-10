using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField] private HitPointsComponentData hitPointsComponentData;
        [SerializeField] private WeaponComponentData weaponComponentData;

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float moveSpeed; //TODO: create config in the future...

        private IMoveableService _moveService;
        
        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponent WeaponComponent {get; private set;}

        private void Awake()
        {
            _moveService = new MoveByRigidbodyVelocity(rigidbody2D);
            
            HitPointsComponent = hitPointsComponentData.Create();
            WeaponComponent = weaponComponentData.Create();
        }

        public void Move(Vector2 direction) => _moveService.Move(direction, moveSpeed);
    }
}