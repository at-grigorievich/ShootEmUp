using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private HitPointsComponentData hitPointsComponentData;
        [SerializeField] private WeaponComponentData weaponComponentData;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float moveSpeed; //TODO: create config in the future...

        private IMoveableService _moveService;

        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }

        private void Awake()
        {
            _moveService = new MoveToDestination(rigidbody2D);

            HitPointsComponent = hitPointsComponentData.Create();
            WeaponComponent = weaponComponentData.Create();
        }

        public void SetActive(bool isActive)
        {
            spriteRenderer.enabled = isActive;
            
            if(isActive == false)
            {
                gameObject.layer = (int)PhysicsLayer.DISABLED;
            }
        }

        public void Reset()
        {
            _moveService.Reset();
        }   

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        public void Place(Vector3 placePosition)
        {
            transform.position = placePosition;
            _moveService.Reset();
        }

        public void Move(Vector2 direction) => _moveService.Move(direction, moveSpeed);
    }
}