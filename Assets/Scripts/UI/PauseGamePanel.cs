using UnityEngine;

namespace ShootEmUp.UI
{
    [RequireComponent(typeof(Canvas))]
    public class PauseGamePanel: MonoBehaviour, IActivateable
    {
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
    }
}