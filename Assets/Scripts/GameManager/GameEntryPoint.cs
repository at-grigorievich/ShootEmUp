using System.Collections;
using System.Collections.Generic;
using ATG.StateMachine;
using ShootEmUp.Helpers;
using ShootEmUp.UI;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private StartGameTimerFactory startGameTimerFactory; 
        [SerializeField] private PauseGamePanel pauseGamePanel;
        [Space(10)]
        [SerializeField] private CharacterFactory characterFactory;

        [SerializeField] private BulletSystemFactory bulletSystemFactory;
        [SerializeField] private EnemySystemFactory enemySystemFactory;

        private StateMachine _sm;
        
        private InputService _inputService;

        private CharacterController _characterController;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;

        private HashSet<IUserInputListener> _userInputListeners = new();
        private HashSet<IStartGameListener> _startGameListeners = new();
        private HashSet<IUpdateGameListener> _updateGameListeners = new();
        private HashSet<IFixedUpdateGameListener> _fixedUpdateGameListeners = new();
        private HashSet<IPauseGameListener> _pauseGameListeners = new();
        private HashSet<IFinishGameListener> _finishGameListeners = new();
        
        private void Awake()
        {
            _sm = new StateMachine();
            _inputService = new InputService(_userInputListeners);

            AddListener(_inputService);
        }

        private void Start()
        {
            _bulletSystem = bulletSystemFactory.Create();
            _characterController = characterFactory.Create(_bulletSystem);
            _enemySystem = enemySystemFactory.Create(_characterController, _bulletSystem);
            
            AddListener(_characterController);
            AddListener(_bulletSystem);
            AddListener(_enemySystem);
            
            IPauseObserver pauseObserver = new InputPauseObserver();
            
            _sm.AddStatementsRange
            (
                new GameStartState(startGameTimerFactory.Create(),_startGameListeners, _sm),
                new GameUpdateState(_updateGameListeners, _fixedUpdateGameListeners, pauseObserver, _sm),
                new GamePauseState(_pauseGameListeners, pauseObserver, pauseGamePanel, _sm)
            );
            
            StartGame();
        }

        private void AddListener(object src)
        {
            if (src is IUserInputListener userInputListener)
            {
                _userInputListeners.Add(userInputListener);
            }

            if (src is IStartGameListener startGameListener)
            {
                _startGameListeners.Add(startGameListener);
            }

            if (src is IUpdateGameListener updateGameListener)
            {
                _updateGameListeners.Add(updateGameListener);
            }

            if (src is IFixedUpdateGameListener fixedUpdateGameListener)
            {
                _fixedUpdateGameListeners.Add(fixedUpdateGameListener);
            }

            if (src is IPauseGameListener pauseGameListener)
            {
                _pauseGameListeners.Add(pauseGameListener);
            }

            if (src is IFinishGameListener finishGameListener)
            {
                _finishGameListeners.Add(finishGameListener);
            }
        }
        
        private void Update()
        {
            _sm.ExecuteMachine();
        }

        private void FixedUpdate()
        {
            _sm.FixedExecuteMachine();
            
            //_enemySystem.FixedUpdate();
        }

        private void StartGame()
        {
            _sm.SwitchState<GameStartState>();
            //_characterController.SetActive(true);
            //_bulletSystem.SetActive(true);
            //_enemySystem.SetActive(true);

            StartCoroutine(SpawnEnemiesWithDelay());

            //_characterController.OnDestroyed += FinishGame;
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
            //Time.timeScale = 0;

            //_characterController.SetActive(false);
            //_bulletSystem.SetActive(false);
            //_enemySystem.SetActive(false);

            //Debug.Log("finish game");
        }
    }
}