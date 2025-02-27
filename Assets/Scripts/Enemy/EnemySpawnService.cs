using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawnService: IStartGameListener, IUpdateGameListener
    {
        private readonly EnemySystem _enemySystem;

        private float _currentTimer;

        public EnemySpawnService(EnemySystem enemySystem)
        {
            _enemySystem = enemySystem;
        }
        
        public void Start()
        {
            ResetTimer();
        }
        
        public void Update()
        {
            if (_currentTimer <= _enemySystem.AddEnemyDelay)
            {
                _currentTimer += Time.deltaTime;
            }
            else
            {
                _enemySystem.AddEnemy();
                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            _currentTimer = 0f;
        }
    }
}