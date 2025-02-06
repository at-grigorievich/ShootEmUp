using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEntryPoint: MonoBehaviour
    {
        [SerializeField] private CharacterFactory characterFactory;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = characterFactory.Create();

            StartGame();
        }

        private void StartGame()
        {
            _characterController.SetActive(true);
        }

        [ContextMenu("Finish Game")]
        private void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;

            _characterController.SetActive(false);
        }
    }
}