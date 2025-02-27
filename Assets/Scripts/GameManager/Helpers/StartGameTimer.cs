using System;
using ShootEmUp.UI;
using UnityEngine;

namespace ShootEmUp.Helpers
{
    [Serializable]
    public class StartGameTimerFactory
    {
        [SerializeField, Header("delay in seconds")] private int delayBeforeStartGame;
        [SerializeField] private StartGamePanel timer;

        public StartGameTimer Create()
        {
            return new StartGameTimer(delayBeforeStartGame, timer);
        }
    }
    
    public class StartGameTimer
    {
        private readonly StartGamePanel _uiPanel;
        
        private readonly int _delayBeforeStartGame;

        private bool _allow;
        private float _currentTimer;

        private int _lastSeconds;
        
        public event Action OnTimerCompleted;
        
        public StartGameTimer(int delayBeforeStartGame, StartGamePanel uiPanel)
        {
            _delayBeforeStartGame = delayBeforeStartGame;
            _uiPanel = uiPanel;
        }
        
        public void Start()
        {
            _currentTimer = _lastSeconds = _delayBeforeStartGame;
            
            _uiPanel.AddStartGameButtonListener(() => _allow = true);
            
            _uiPanel.UpdateTimer(_lastSeconds);
            _uiPanel.SetActive(true);
        }
        
        public void Stop()
        {
            _allow = false;
            
            _uiPanel.SetActive(false);
            _uiPanel.RemoveAllStartGameButtonListeners();
        }
        
        public void UpdateTimer()
        {
            if(_allow == false) return;
            
            if (_currentTimer > 0)
            {
                _currentTimer -= Time.deltaTime;

                int currentSeconds = (int)(_currentTimer % 60f);
                
                if (_lastSeconds != currentSeconds)
                {
                    _uiPanel.UpdateTimer(currentSeconds + 1);
                }
                
                _lastSeconds = currentSeconds;
            }
            else
            {
                OnTimerCompleted?.Invoke();
                Stop();
            }
        }
    }
}