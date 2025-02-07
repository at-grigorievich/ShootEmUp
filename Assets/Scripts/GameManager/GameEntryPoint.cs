using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEntryPoint: MonoBehaviour
    {
        [SerializeField] private CharacterFactory characterFactory;

        [SerializeField] private BulletSystemFactory bulletSystemFactory;

        private InputService _inputService;

        private CharacterController _characterController;
        private BulletSystem _bulletSystem;

        private void Awake()
        {
            _inputService = new InputService();
        }

        private void Start()
        {
            _bulletSystem = bulletSystemFactory.Create();
            _characterController = characterFactory.Create(_inputService, _bulletSystem);
            
            StartGame();
        }

        private void Update()
        {
            _inputService.Update();
            _characterController.Update();
        }

        private void FixedUpdate()
        {
            _bulletSystem.FixedUpdate();
        }

        private void StartGame()
        {
            _characterController.SetActive(true);
            _bulletSystem.SetActive(true);
        }

        [ContextMenu("Finish Game")]
        private void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;

            _characterController.SetActive(false);
            _bulletSystem.SetActive(false);
        }
    }
}