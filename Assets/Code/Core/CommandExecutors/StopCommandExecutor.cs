using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"{name} stop");
        }
    }
}