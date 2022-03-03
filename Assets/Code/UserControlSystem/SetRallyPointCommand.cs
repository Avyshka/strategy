using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem
{
    public class SetRallyPointCommand : ISetRallyPointCommand
    {
        public Vector3 RallyPoint { get; }

        public SetRallyPointCommand(Vector3 rallyPoint)
        {
            RallyPoint = rallyPoint;
        }
    }
}