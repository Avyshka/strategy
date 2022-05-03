using System;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.Utils.AssetsInjector;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Model.CommandCreators
{
    public class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new StopCommand()));
        }
    }
}