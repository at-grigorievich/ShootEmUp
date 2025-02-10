using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyController
    {
        private readonly EnemyView _view;

        private readonly BulletSystem _bulletSystem;
        
        private readonly IHpEditor _hpEditor;

        private Vector2 _destination;
        private ITargeteable _target;

        public EnemyController(EnemyView view, BulletSystem bulletSystem)
        {
            _view = view;

            _bulletSystem = bulletSystem;
            _hpEditor = new HitPointsService(_view.HitPointsComponent);
        }

        public void SetActive(bool isActive)
        {
            _view.SetActive(isActive);

            if(isActive == true)
            {
                
            }
            else
            {
                _view.Reset();
            }
        }

        public void FixedUpdate()
        {
            _view.Move(_destination);
        }

        public void SetParent(Transform parent)
        {
            _view.SetParent(parent);
        }

        public void SetPosition(Vector3 position)
        {
            _view.Place(position);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
        }

        public void SetTarget(ITargeteable target)
        {
            _target = target;
        }
    }
}