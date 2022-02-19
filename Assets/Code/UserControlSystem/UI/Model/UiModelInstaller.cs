using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model.CommandCreators;
using Aivagames.Strategy.Utils.AssetsInjector;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    public class UiModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetsContext _legacyContext;
        [SerializeField] private Vector3Value _groundClicksRMB;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_legacyContext);
            Container.Bind<Vector3Value>().FromInstance(_groundClicksRMB);

            Container
                .Bind<CommandCreatorBase<IProduceUnitCommand>>()
                .To<ProduceUnitCommandCommandCreator>()
                .AsTransient();
            Container
                .Bind<CommandCreatorBase<IAttackCommand>>()
                .To<AttackCommandCreator>()
                .AsTransient();
            Container
                .Bind<CommandCreatorBase<IMoveCommand>>()
                .To<MoveCommandCreator>()
                .AsTransient();
            Container
                .Bind<CommandCreatorBase<IStopCommand>>()
                .To<StopCommandCreator>()
                .AsTransient();
            Container
                .Bind<CommandCreatorBase<IPatrolCommand>>()
                .To<PatrolCommandCreator>()
                .AsTransient();

            Container.Bind<CommandButtonsModel>().AsTransient();
        }
    }
}