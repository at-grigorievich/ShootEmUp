using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyView : MonoBehaviour, IDamageable
    {
        [SerializeField] private HitPointsComponentData hitPointsComponentData;
        [SerializeField] private WeaponComponentData weaponComponentData;
        [SerializeField] private TeamComponentData teamComponentData;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float moveSpeed; //TODO: create config in the future...

        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponentData WeaponComponentData => weaponComponentData;

        public bool IsPlayer => teamComponentData.IsPlayer;

        public event Action<int> OnDamaged; // Можно сделать с EventHandler...

        private void Awake()
        {
            HitPointsComponent = hitPointsComponentData.Create();
        }

        public void SetActive(bool isActive)
        {
            spriteRenderer.enabled = isActive;
            
            SetLayer(isActive);
        }

        public void SetLayer(bool isActive)
        {
            if(isActive == true)
            {
                gameObject.layer = (int)PhysicsLayer.ENEMY;
            }
            else
            {
                gameObject.layer = (int)PhysicsLayer.DISABLED;
            }
        }
        
        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        public void Place(Vector3 placePosition)
        {
            transform.position = placePosition;
        }
        
        public void TakeDamage(int damage) => OnDamaged?.Invoke(damage);
    
        public IMoveableService CreateMovement() => new MoveToDestination(rigidbody2D, moveSpeed);
    }
}