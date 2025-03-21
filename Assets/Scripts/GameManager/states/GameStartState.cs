using System.Collections.Generic;
using ATG.StateMachine;
using ShootEmUp.Helpers;

namespace ShootEmUp
{
    public sealed class GameStartState : Statement
    {
        private readonly StartGameTimer _startGameTimer;
        private readonly IEnumerable<IStartGameListener> _listeners;
        
        public GameStartState(StartGameTimer startGameTimer,
            IEnumerable<IStartGameListener> listeners, IStateSwitcher sw) : base(sw)
        {
            _listeners = listeners;
            _startGameTimer = startGameTimer;
        }

        public override void Enter()
        {
            _startGameTimer.OnTimerCompleted += InvokeListeners;
            _startGameTimer.Start();
        }

        public override void Exit()
        {
            _startGameTimer.OnTimerCompleted -= InvokeListeners;
            _startGameTimer.Stop();
            
            base.Exit();
        }

        public override void Execute()
        {
            _startGameTimer.UpdateTimer();
        }

        public override void FixedExecute() { }

        private void InvokeListeners()
        {
            foreach (var startGameListener in _listeners)
            {
                startGameListener.Start();
            }
            
            _stateSwitcher.SwitchState<GameUpdateState>();
        }
    }
}
