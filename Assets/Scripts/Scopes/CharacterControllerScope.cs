using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public class CharacterControllerScope: LifetimeScope
    {
        [SerializeField] private CharacterView view;
        [SerializeField] private Rigidbody2D viewRigidbody2D;
        [SerializeField] private float moveSpeed;
        [Space(10)]
        [SerializeField] private HitPointsComponentData hitPointsComponentData;
        [SerializeField] private WeaponComponentData weaponComponentData;
        [SerializeField] private TeamComponentData teamComponentData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IHpEditor, HitPointsService>(Lifetime.Singleton)
                .WithParameter(hitPointsComponentData.Create());
            
            builder.Register<IMoveableService, MoveByInput>(Lifetime.Singleton)
                .WithParameter(viewRigidbody2D)
                .WithParameter("speed", moveSpeed);

            builder.Register<IWeapon, InputWeaponService>(Lifetime.Singleton)
                .WithParameter(weaponComponentData.Create())
                .WithParameter(teamComponentData.IsPlayer);

            builder.Register<CharacterController>(Lifetime.Singleton)
                .WithParameter(view)
                .WithParameter(teamComponentData)
                .AsSelf().AsImplementedInterfaces();
            
            builder.RegisterBuildCallback(container =>
            {
                var characterController = container.Resolve<CharacterController>();
                var listenersDispatcher = container.Resolve<GameCycleListenersDispatcher>();
                var enemySystem = container.Resolve<EnemySystem>();

                listenersDispatcher.AddListener(characterController);
                enemySystem.SetTarget(characterController);
            });
        }
    }
}