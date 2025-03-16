using ShootEmUp.Helpers;
using ShootEmUp.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class GameSceneScope : LifetimeScope
    {
        [SerializeField] private PauseGamePanel pauseGamePanel;
        [Space(5)]
        [SerializeField] private StartGameTimerFactory startGameTimerFactory;
        [SerializeField] private BulletSystemFactory bulletSystemFactory;
        [SerializeField] private EnemySystemFactory enemySystemFactory;
        [SerializeField] private LevelBackgroundFactory levelBackgroundFactory;

        protected override void Configure(IContainerBuilder builder)
        {
            startGameTimerFactory.Register(builder);
            bulletSystemFactory.Register(builder);
            enemySystemFactory.Register(builder);
            levelBackgroundFactory.Register(builder);

            builder.Register<GameFinalizator>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<GameCycleListenersDispatcher>(Lifetime.Singleton)
                .AsSelf().AsImplementedInterfaces();
            
            builder.Register<InputService>(Lifetime.Singleton)
                .AsSelf().AsImplementedInterfaces();

            builder.RegisterEntryPoint<GameEntryPoint>()
                .WithParameter(pauseGamePanel);
        }
    }
}