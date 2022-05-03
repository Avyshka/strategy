using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface IProduceUnitCommand : ICommand, IIconHolder
    {
        GameObject UnitPrefab { get; }
        float ProductionTime { get; }
        string UnitName { get; }
    }
}