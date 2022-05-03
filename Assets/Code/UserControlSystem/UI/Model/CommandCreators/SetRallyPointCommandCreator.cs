using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem.UI.Model.CommandCreators
{
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument) => new SetRallyPointCommand(argument);
    }
}