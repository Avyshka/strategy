using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.Core
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public Sprite Icon { get; }
        public string UnitName { get; }
        public float TimeLeft { get; set; }
        public float ProductionTime { get; }
        public GameObject UnitPrefab { get; }

        public UnitProductionTask(float time, Sprite icon, GameObject unitPrefab, string unitName)
        {
            Icon = icon;
            ProductionTime = time;
            TimeLeft = 0;
            UnitPrefab = unitPrefab;
            UnitName = unitName;
        }
    }
}