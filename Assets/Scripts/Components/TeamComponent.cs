using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IDamageable
    {
        public bool IsPlayer { get; }
        event Action<int> OnDamaged; // Можно сделать с EventHandler...

        public void TakeDamage(int damage);
    }

    [Serializable]
    public sealed class TeamComponentData
    {
        [field: SerializeField] public bool IsPlayer {get; private set;}
    }
}