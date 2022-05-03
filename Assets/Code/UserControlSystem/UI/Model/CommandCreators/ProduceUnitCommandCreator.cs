using System;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.Utils.AssetsInjector;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Model.CommandCreators
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;
        
        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new ProduceUnitCommandHeir()));
        }
    }
}