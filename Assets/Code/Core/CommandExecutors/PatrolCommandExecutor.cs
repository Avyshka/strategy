using System.Threading.Tasks;
using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{name} patrolling from {command.From} to {command.To}");
        }
    }
}