using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterView : MonoBehaviour, IActivateable, IDamageable
    {
        [SerializeField] private HitPointsComponentData hitPointsComponentData;
        [SerializeField] private WeaponComponentData weaponComponentData;
        [SerializeField] private TeamComponentData teamComponentData;
        [Space(10)]
        [SerializeField] private Renderer renderer;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float moveSpeed; //TODO: create config in the future...

        public HitPointsComponent HitPointsComponent { get; private set; }
        public WeaponComponentData WeaponComponentData => weaponComponentData;

        public bool IsPlayer => teamComponentData.IsPlayer;

        public event Action<int> OnDamaged;

        private void Awake()
        {
            HitPointsComponent = hitPointsComponentData.Create();
        }

        public bool IsActive { get; private set; }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            
            if (isActive == true)
            {
                gameObject.layer = (int)PhysicsLayer.CHARACTER;
            }
            else
            {
                gameObject.layer = (int)PhysicsLayer.DISABLED;
            }
        }

        public void SetVisible(bool isVisible)
        {
            renderer.enabled = isVisible;
        }

        public void PlaceDefaultPosition()
        {
            transform.position = new Vector3(0f, -3.5f, 0f);
        }
        
        public void TakeDamage(int damage) => OnDamaged?.Invoke(damage);

        public IMoveableService CreateMovement() => new MoveByInput(rigidbody2D, moveSpeed);
    }
}