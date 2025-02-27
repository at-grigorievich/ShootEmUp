﻿using System.Collections.Generic;
using ATG.StateMachine;
using ShootEmUp.UI;

namespace ShootEmUp
{
    public sealed class GamePauseState: Statement
    {
        private readonly IPauseObserver _pauseObserver;
        
        private readonly IEnumerable<IPauseGameListener> _listeners;

        private readonly PauseGamePanel _ui;
        
        public GamePauseState(IEnumerable<IPauseGameListener> listeners, 
            IPauseObserver pauseObserver, PauseGamePanel ui,
            IStateSwitcher sw) : base(sw)
        {
            _listeners = listeners;
            _pauseObserver = pauseObserver;
            _ui = ui;
        }

        public override void Enter()
        {
            _ui.SetActive(true);

            foreach (var pauseGameListener in _listeners)
            {
                pauseGameListener.Pause();
            }
        }

        public override void Exit()
        {
            _ui.SetActive(false);
            base.Exit();
        }

        public override void Execute()
        {
            if (_pauseObserver.IsPausePressed == true)
            {
                SwitchToUpdate();
                return;
            }
        }
        public override void FixedExecute() { }

        private void SwitchToUpdate()
        {
            foreach (var pauseGameListener in _listeners)
            {
                pauseGameListener.Resume();
            }

            _stateSwitcher.SwitchState<GameUpdateState>();
        }
    }
}