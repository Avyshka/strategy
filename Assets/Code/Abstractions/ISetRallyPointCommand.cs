using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface ISetRallyPointCommand : ICommand
    {
        Vector3 RallyPoint { get; }
    }
}