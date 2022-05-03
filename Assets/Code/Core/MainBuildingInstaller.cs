using Aivagames.Strategy.Abstractions;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class MainBuildingInstaller : MonoInstaller
    {
        [SerializeField] private FactionMemberParallelInfoUpdater _factionMemberParallelInfoUpdater;

        public override void InstallBindings()
        {
            Container.Bind<ITickable>().FromInstance(_factionMemberParallelInfoUpdater);
            Container.Bind<IFactionMember>().FromComponentInChildren();
        }
    }
}