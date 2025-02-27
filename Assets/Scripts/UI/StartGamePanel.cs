using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI
{
    [RequireComponent(typeof(Canvas))]
    public class StartGamePanel: MonoBehaviour, IActivateable
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private TextMeshProUGUI timerInfo;
        [SerializeField] private TextMeshProUGUI timerOutput;
        
        private Canvas _canvas;
        
        public bool IsActive { get; private set; }
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            SetActive(false);
        }

        public void AddStartGameButtonListener(Action listener)
        {
            startGameButton.onClick.AddListener(() => listener?.Invoke());
            startGameButton.onClick.AddListener(SwitchStartGameButtonToTimer);
        }

        public void RemoveAllStartGameButtonListeners()
        {
            startGameButton.onClick.RemoveAllListeners();
        }
        
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            _canvas.enabled = isActive;
            
            startGameButton.gameObject.SetActive(isActive);
            
            timerInfo.enabled = false;
            timerOutput.enabled = false;
        }

        public void UpdateTimer(int timerValue)
        {
            timerOutput.text = $"{timerValue.ToString()} сек.";
        }

        private void SwitchStartGameButtonToTimer()
        {
            startGameButton.gameObject.SetActive(false);
            
            timerInfo.enabled = true;
            timerOutput.enabled = true;
        }
    }
}