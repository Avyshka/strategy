using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem;
using UnityEngine;

namespace Aivagames.Strategy.Core
{
    public class Chomper : MonoBehaviour, ISelectable, IAttackable, IDamageDealer, IUnit
    {
        [SerializeField] private float _maxHealth = 50;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Transform _pivotPoint;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;
        [SerializeField] private int _damage = 25;

        private float _health = 50;

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Transform PivotPoint => _pivotPoint;
        public Sprite Icon => _icon;
        public int Damage => _damage;
        
        public void ReceiveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }

            _health -= amount;
            if (_health <= 0)
            {
                _animator.SetTrigger("Dead");
                Invoke(nameof(destroy), 1f);
            }
        }

        private async void destroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }
    }
}