using Code.Utils;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy/" + nameof(Vector3Value), order = 0)]
    public class Vector3Value : StatelessScriptableObjectValueBase<Vector3>
    {
    }
}