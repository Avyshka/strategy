using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [SerializeField] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
}