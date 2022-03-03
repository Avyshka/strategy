using Aivagames.Strategy.Abstractions;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] private CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;

        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
        }

        public void Clear()
        {
        }
    }
}