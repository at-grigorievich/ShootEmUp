using System.Collections.Generic;
using ATG.StateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameUpdateState: Statement
    {
        private readonly IPauseObserver _pauseObserver;
        private readonly IGameFinalizator _gameFinalizator;
        
        private readonly IEnumerable<IUpdateGameListener> _listeners;
        private readonly IEnumerable<IFixedUpdateGameListener> _fixedListeners;
        
        public GameUpdateState(IEnumerable<IUpdateGameListener> listeners,
            IEnumerable<IFixedUpdateGameListener> fixedListeners, 
            IPauseObserver pauseObserver, IGameFinalizator gameFinalizator,
            IStateSwitcher sw) : base(sw)
        {
            _listeners = listeners;
            _fixedListeners = fixedListeners;
            
            _pauseObserver = pauseObserver;
            _gameFinalizator = gameFinalizator;
        }

        public override void Enter()
        {
            _gameFinalizator.OnFinished += SwitchToFinish;
        }

        public override void Exit()
        {
            _gameFinalizator.OnFinished -= SwitchToFinish;
            base.Exit();
        }

        public override void Execute()
        {
            if (_pauseObserver.IsPausePressed == true)
            {
                SwitchToPause();
                return;
            }
            
            foreach (var updateGameListener in _listeners)
            {
                updateGameListener.Update();
            }
        }

        public override void FixedExecute()
        {
            foreach (var fixedUpdateGameListener in _fixedListeners)
            {
                fixedUpdateGameListener.FixedUpdate();
            }
        }

        private void SwitchToPause()
        {
            _stateSwitcher.SwitchState<GamePauseState>();
        }

        private void SwitchToFinish()
        {
            _stateSwitcher.SwitchState<GameFinishState>();
        }
    }
}