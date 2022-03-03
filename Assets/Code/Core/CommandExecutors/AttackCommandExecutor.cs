using System.Threading.Tasks;
using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log(
                $"{name} attacking {command.Target} with {command.Target.Health}/{command.Target.MaxHealth} hps!");
        }
    }
}