using System;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy/" + nameof(Vector3Value), order = 0)]
    public class Vector3Value : ScriptableObject
    {
        public Vector3 CurrentValue { get; private set; }
        public Action<Vector3> OnNewValue;

        public void SetValue(Vector3 value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }
    }
}