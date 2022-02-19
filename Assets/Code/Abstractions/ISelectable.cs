using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface ISelectable : IHealthHolder
    {
        Transform PivotPoint { get; }
        Sprite Icon { get; }
    }
}