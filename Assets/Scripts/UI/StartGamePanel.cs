using TMPro;
using UnityEngine;

namespace ShootEmUp.UI
{
    [RequireComponent(typeof(Canvas))]
    public class StartGamePanel: MonoBehaviour, IActivateable
    {
        [SerializeField] private TextMeshProUGUI timerOutput;
        
        private Canvas _canvas;

        public bool IsActive { get; private set; }
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            SetActive(false);
        }
        
        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            _canvas.enabled = isActive;
        }

        public void UpdateTimer(int timerValue)
        {
            timerOutput.text = $"{timerValue.ToString()} сек.";
        }
    }
}