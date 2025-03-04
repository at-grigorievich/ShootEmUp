using ATG.StateMachine;
using ShootEmUp.Helpers;
using ShootEmUp.UI;
using UnityEngine;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class GameEntryPoint: IStartable, ITickable, IFixedTickable
    {
        private readonly GameCycleListenersDispatcher _listenerDispatcher;
        
        private readonly StateMachine _sm;

        public GameEntryPoint(StartGameTimer startGameTimer, IGameFinalizeHandler gameFinalizeHandler,
            GameCycleListenersDispatcher listenersDispatcher, 
            InputService inputService,
            PauseGamePanel pauseGamePanel)
        {
            _listenerDispatcher = listenersDispatcher;
            
            _sm = new StateMachine();
            _sm.AddStatementsRange
            (
                new GameStartState(startGameTimer, listenersDispatcher.StartGameListeners,  _sm),
                new GameUpdateState(listenersDispatcher.UpdateGameListeners, listenersDispatcher.FixedUpdateGameListeners, 
                    inputService, gameFinalizeHandler, _sm),
                new GamePauseState(listenersDispatcher.PauseGameListeners, inputService, pauseGamePanel, _sm),
                new GameFinishState(listenersDispatcher.FinishGameListeners, _sm)
            );
        }
        
        public void Start()
        {
            StartGame();
        }
        
        public void Tick()
        {
            _sm.ExecuteMachine();
        }

        public void FixedTick()
        {
            _sm.FixedExecuteMachine();
        }

        private void StartGame()
        {
            _sm.SwitchState<GameStartState>();
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