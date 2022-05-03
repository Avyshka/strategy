using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy/" + nameof(AttackableValue), order = 0)]
    public class AttackableValue : ScriptableObjectValueBase<IAttackable>
    {
    }
}