using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.Core
{
    public class Chomper : MonoBehaviour, ISelectable
    {
        [SerializeField] private float _maxHealth = 50;
        [SerializeField] private Sprite _icon;

        private float _health = 50;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
    }
}