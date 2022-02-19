using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(SelectableValue), menuName = "Strategy/" + nameof(SelectableValue), order = 0)]
    public class SelectableValue : ScriptableObjectValueBase<ISelectable>
    {
    }
}