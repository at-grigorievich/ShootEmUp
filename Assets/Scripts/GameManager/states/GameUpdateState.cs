using System.Collections.Generic;
using ATG.StateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameUpdateState: Statement
    {
        private readonly IPauseObserver _pauseObserver;
        
        private readonly IEnumerable<IUpdateGameListener> _listeners;
        private readonly IEnumerable<IFixedUpdateGameListener> _fixedListeners;
        
        public GameUpdateState(IEnumerable<IUpdateGameListener> listeners,
            IEnumerable<IFixedUpdateGameListener> fixedListeners, IPauseObserver pauseObserver,
            IStateSwitcher sw) : base(sw)
        {
            _listeners = listeners;
            _fixedListeners = fixedListeners;
            
            _pauseObserver = pauseObserver;
        }

        public override void Enter()
        {
        }

        public override void Execute()
        {
            if (_pauseObserver.IsPausePressed == true)
            {
                SwithToPause();
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

        private void SwithToPause()
        {
            _stateSwitcher.SwitchState<GamePauseState>();
        }
    }
}