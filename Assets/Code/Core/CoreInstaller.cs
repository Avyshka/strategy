using Aivagames.Strategy.Abstractions;
using Zenject;

namespace Aivagames.Strategy.Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.Bind<IGameStatus>().FromInstance(GetComponent<IGameStatus>());
        }
    }
}