using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{name} move");
        }
    }
}