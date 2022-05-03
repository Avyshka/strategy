using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface ISelectable : IHealthHolder, IIconHolder
    {
        Transform PivotPoint { get; }
    }
}