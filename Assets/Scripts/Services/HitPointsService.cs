using System;

namespace ShootEmUp
{
    public interface IHpEditor
    {
        event Action OnHpEmpty;

        void ResetHp();
        void TakeDamage(int damage);
    }

    public class HitPointsService: IHpEditor
    {
        private readonly HitPointsComponent _data;
        
        public event Action OnHpEmpty;

        public HitPointsService(HitPointsComponent data)
        {
            _data = data;
        }

        public void ResetHp()
        {
            _data.ResetHp();
        }

        public void TakeDamage(int damage)
        {
            _data.TakeDamage(damage);

            if(_data.IsHitPointsExists() == false)
            {
                OnHpEmpty?.Invoke();
            }
        }
    }
}