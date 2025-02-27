using System.Collections.Generic;
using ATG.StateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameFinishState: Statement
    {
        private readonly IEnumerable<IFinishGameListener> _listeners;
        private readonly float _waitToRestart;

        private float _currentTimer = 0f;
        
        public GameFinishState(IEnumerable<IFinishGameListener> listeners,IStateSwitcher sw)
            : base(sw)
        {
            _listeners = listeners;
            _waitToRestart = 8f;
        }

        public override void Enter()
        {
            Debug.Log("finish game");
            
            _currentTimer = 0f;

            foreach (var finishGameListener in _listeners)
            {
                finishGameListener.Finish();
            }
        }

        public override void Execute()
        {
            _currentTimer += Time.deltaTime;

            if (_currentTimer >= _waitToRestart)
            {
                SwitchToStart();
            }
        }

        public override void FixedExecute()
        {
        }

        private void SwitchToStart()
        {
            _stateSwitcher.SwitchState<GameStartState>();
        }
    }
}