using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.Core
{
    public class Chomper : MonoBehaviour, ISelectable, IAttackable
    {
        [SerializeField] private float _maxHealth = 50;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Transform _pivotPoint;

        private float _health = 50;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Transform PivotPoint => _pivotPoint;
        public Sprite Icon => _icon;
    }
}