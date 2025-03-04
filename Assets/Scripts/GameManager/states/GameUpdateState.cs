using System.Collections.Generic;
using ATG.StateMachine;

namespace ShootEmUp
{
    public sealed class GameUpdateState: Statement
    {
        private readonly IPauseObserver _pauseObserver;
        private readonly IGameFinalizeHandler _gameFinalizeHandler;
        
        private readonly IEnumerable<IUpdateGameListener> _listeners;
        private readonly IEnumerable<IFixedUpdateGameListener> _fixedListeners;
        
        public GameUpdateState(IEnumerable<IUpdateGameListener> listeners,
            IEnumerable<IFixedUpdateGameListener> fixedListeners, 
            IPauseObserver pauseObserver, IGameFinalizeHandler gameFinalizeHandler,
            IStateSwitcher sw) : base(sw)
        {
            _listeners = listeners;
            _fixedListeners = fixedListeners;
            
            _pauseObserver = pauseObserver;
            _gameFinalizeHandler = gameFinalizeHandler;
        }

        public override void Enter()
        {
            _gameFinalizeHandler.OnFinalized += SwitchToFinish;
        }

        public override void Exit()
        {
            _gameFinalizeHandler.OnFinalized -= SwitchToFinish;
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