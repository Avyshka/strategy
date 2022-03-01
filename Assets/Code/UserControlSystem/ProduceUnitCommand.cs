using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.Utils.AssetsInjector;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        [InjectAsset("Chomper")] private GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
        
        [Inject(Id = "Chomper")] public string UnitName { get; }
        [Inject(Id = "Chomper")] public Sprite Icon { get; }
        [Inject(Id = "Chomper")] public float ProductionTime { get; }
    }
}