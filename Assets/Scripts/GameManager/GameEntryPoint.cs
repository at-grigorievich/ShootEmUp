using System.Collections;
using System.Collections.Generic;
using ATG.StateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private CharacterFactory characterFactory;

        [SerializeField] private BulletSystemFactory bulletSystemFactory;
        [SerializeField] private EnemySystemFactory enemySystemFactory;

        private StateMachine _sm;
        
        private InputService _inputService;

        private CharacterController _characterController;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        
        private HashSet<IStartGameListener> _startGameListeners;
        private HashSet<IUpdateGameListener> _updateGameListeners;
        private HashSet<IPauseGameListener> _pauseGameListeners;
        private HashSet<IFinishGameListener> _finishGameListeners;
        
        private void Awake()
        {
            _sm = new StateMachine();
            _inputService = new InputService();
        }

        private void Start()
        {
            _bulletSystem = bulletSystemFactory.Create();
            _characterController = characterFactory.Create(_inputService, _bulletSystem);
            _enemySystem = enemySystemFactory.Create(_characterController, _bulletSystem);

            StartGame();
        }

        private void Update()
        {
            _sm.ExecuteMachine();
            
            _inputService.Update();
            _characterController.Update();
            _bulletSystem.Update();
        }

        private void FixedUpdate()
        {
            _sm.FixedExecuteMachine();
            
            _enemySystem.FixedUpdate();
        }

        private void StartGame()
        {
            _characterController.SetActive(true);
            _bulletSystem.SetActive(true);
            _enemySystem.SetActive(true);

            StartCoroutine(SpawnEnemiesWithDelay());

            _characterController.OnDestroyed += FinishGame;
        }

        private IEnumerator SpawnEnemiesWithDelay()
        {
            while(true)
            {
                yield return new WaitForSeconds(_enemySystem.AddEnemyDelay);
                _enemySystem.AddEnemy();
            }
        }

        private void FinishGame()
        {
            Time.timeScale = 0;

            _characterController.SetActive(false);
            _bulletSystem.SetActive(false);
            _enemySystem.SetActive(false);

            Debug.Log("finish game");
        }
    }
}