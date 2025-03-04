﻿using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class GameCycleListenersDispatcher
    {
        public readonly HashSet<IUserInputListener> UserInputListeners;
        public readonly HashSet<IStartGameListener> StartGameListeners;
        public readonly HashSet<IUpdateGameListener> UpdateGameListeners;
        public readonly HashSet<IFixedUpdateGameListener> FixedUpdateGameListeners;
        public readonly HashSet<IPauseGameListener> PauseGameListeners;
        public readonly HashSet<IFinishGameListener> FinishGameListeners;

        public GameCycleListenersDispatcher(IEnumerable<IUserInputListener> userInputListeners,
            IEnumerable<IStartGameListener> startGameListeners, IEnumerable<IUpdateGameListener> updateGameListeners,
            IEnumerable<IFixedUpdateGameListener> fixedUpdateGameListeners,
            IEnumerable<IPauseGameListener> pauseGameListeners,
            IEnumerable<IFinishGameListener> finishGameListeners)
        {
            UserInputListeners = new HashSet<IUserInputListener>(userInputListeners);
            StartGameListeners = new HashSet<IStartGameListener>(startGameListeners);
            UpdateGameListeners = new HashSet<IUpdateGameListener>(updateGameListeners);
            FixedUpdateGameListeners = new HashSet<IFixedUpdateGameListener>(fixedUpdateGameListeners);
            PauseGameListeners = new HashSet<IPauseGameListener>(pauseGameListeners);
            FinishGameListeners = new HashSet<IFinishGameListener>(finishGameListeners);
        }
        
        public void AddListener(object src)
        {
            if (src is IUserInputListener userInputListener)
            {
                UserInputListeners.Add(userInputListener);
            }

            if (src is IStartGameListener startGameListener)
            {
                StartGameListeners.Add(startGameListener);
            }

            if (src is IUpdateGameListener updateGameListener)
            {
                UpdateGameListeners.Add(updateGameListener);
            }

            if (src is IFixedUpdateGameListener fixedUpdateGameListener)
            {
                FixedUpdateGameListeners.Add(fixedUpdateGameListener);
            }

            if (src is IPauseGameListener pauseGameListener)
            {
                PauseGameListeners.Add(pauseGameListener);
            }

            if (src is IFinishGameListener finishGameListener)
            {
                FinishGameListeners.Add(finishGameListener);
            }
        }
    }
}