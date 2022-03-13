using Aivagames.Strategy.Abstractions;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] private CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
        [Inject] private CommandExecutorBase<ISetRallyPointCommand> _setRallyPointCommandExecutor;

        public ICommand CurrentCommand => default;

        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
            await _setRallyPointCommandExecutor.TryExecuteCommand(command);
        }

        public void Clear()
        {
        }
    }
}