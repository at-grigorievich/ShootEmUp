using System.Collections.Generic;
using ATG.StateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameUpdateState: Statement
    {
        private readonly IEnumerable<IUpdateGameListener> _listeners;
        private readonly IEnumerable<IFixedUpdateGameListener> _fixedListeners;
        
        public GameUpdateState(IEnumerable<IUpdateGameListener> listeners,
            IEnumerable<IFixedUpdateGameListener> fixedListeners, IStateSwitcher sw) : base(sw)
        {
            _listeners = listeners;
            _fixedListeners = fixedListeners;
        }

        public override void Enter()
        {
        }

        public override void Execute()
        {
            foreach (var updateGameListener in _listeners)
            {
                updateGameListener.Update();
            }
        }

        public override void FixedExecute()
        {
            Debug.Log("fixed update");
            foreach (var fixedUpdateGameListener in _fixedListeners)
            {
                fixedUpdateGameListener.FixedUpdate();
            }
        }
    }
}