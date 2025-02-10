using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private CharacterFactory characterFactory;

        [SerializeField] private BulletSystemFactory bulletSystemFactory;
        [SerializeField] private EnemySystemFactory enemySystemFactory;

        private InputService _inputService;

        private CharacterController _characterController;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;

        private void Awake()
        {
            _inputService = new InputService();
        }

        private void Start()
        {
            _bulletSystem = bulletSystemFactory.Create();
            _characterController = characterFactory.Create(_inputService, _bulletSystem);
            _enemySystem = enemySystemFactory.Create(_characterController, _bulletSystem);

            StartGame();
        }

        private void Update()
        {
            _inputService.Update();
            _characterController.Update();
            _bulletSystem.Update();
        }

        private void FixedUpdate()
        {
            _enemySystem.FixedUpdate();
        }

        private void StartGame()
        {
            _characterController.SetActive(true);
            _bulletSystem.SetActive(true);
            _enemySystem.SetActive(true);

            StartCoroutine(StartEnemySystemWithDelay(1f));
        }

        private IEnumerator StartEnemySystemWithDelay(float delay)
        {
            for (int i = 0; i < _enemySystem.NeedCount; i++)
            {
                yield return new WaitForSeconds(delay);
                _enemySystem.AddEnemy();
            }
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