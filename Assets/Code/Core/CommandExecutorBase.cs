using System.Threading.Tasks;
using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor<T> where T : class, ICommand
    {
        public async Task TryExecuteCommand(object command)
        {
            if (command is T specificCommand)
            {
                await ExecuteSpecificCommand(specificCommand);
            }
        }

        public abstract Task ExecuteSpecificCommand(T command);
    }
}