using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void DealDamage(BulletView bullet, GameObject other)
        {
            if (other.TryGetComponent(out IDamageable damageable) == true)
            {
                DealDamage(bullet, damageable);
            }
        }

        internal static void DealDamage(BulletView bullet, IDamageable damageable)
        {
            if (bullet.IsPlayer == damageable.IsPlayer) return;
            damageable.TakeDamage(bullet.Damage);
        }
    }
}