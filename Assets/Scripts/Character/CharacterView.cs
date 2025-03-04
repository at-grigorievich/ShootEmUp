using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterView : MonoBehaviour, IActivateable, IDamageable
    {
        [SerializeField] private Renderer renderer;
        
        public bool IsPlayer { get; set; }

        public bool IsActive { get; private set; }
        
        public event Action<int> OnDamaged;

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
    }
}