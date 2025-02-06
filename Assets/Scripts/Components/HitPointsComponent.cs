using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class HitPointsComponentData
    {
        [SerializeField] private int hitPoints;

        public HitPointsComponent Create() => new HitPointsComponent(hitPoints);
    }

    public sealed class HitPointsComponent
    {
        private readonly int _defaultHitPoints;

        public int HitPoints {get; private set;}
        
        public HitPointsComponent(int hitPoints)
        {
            _defaultHitPoints = hitPoints;
            HitPoints = _defaultHitPoints;
        }

        public bool IsHitPointsExists() => HitPoints > 0;
        
        public void ResetHp()
        {
            HitPoints = _defaultHitPoints;
        }

        public void TakeDamage(int damage)
        {
            HitPoints = Mathf.Clamp(HitPoints - damage, 0, _defaultHitPoints);
        }
    }
}