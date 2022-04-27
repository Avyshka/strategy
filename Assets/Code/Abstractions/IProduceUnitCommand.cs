using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }
}