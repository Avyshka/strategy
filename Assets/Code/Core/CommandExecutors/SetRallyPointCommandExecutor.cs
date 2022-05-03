using System.Threading.Tasks;
using Aivagames.Strategy.Core;

namespace Aivagames.Strategy.Abstractions
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override async Task ExecuteSpecificCommand(ISetRallyPointCommand command)
        {
            GetComponent<MainBuilding>().RallyPoint = command.RallyPoint;
        }
    }
}