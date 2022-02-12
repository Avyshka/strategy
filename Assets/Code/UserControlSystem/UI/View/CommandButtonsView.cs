using System;
using System.Collections.Generic;
using System.Linq;
using Aivagames.Strategy.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.Strategy.UserControlSystem.UI.View
{
    public class CommandButtonsView : MonoBehaviour
    {
        public Action<ICommandExecutor> OnClick;

        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _holdButton;
        [SerializeField] private GameObject _produceUnitButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>
            {
                {typeof(CommandExecutorBase<IMoveCommand>), _moveButton},
                {typeof(CommandExecutorBase<IStopCommand>), _stopButton},
                {typeof(CommandExecutorBase<IAttackCommand>), _attackButton},
                {typeof(CommandExecutorBase<IPatrolCommand>), _holdButton},
                {typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton}
            };
        }

        public void MakeLayout(IEnumerable<ICommandExecutor> commandsExecutors)
        {
            foreach (var currentExecutor in commandsExecutors)
            {
                var buttonGameObject = _buttonsByExecutorType
                    .First(type => type.Key.IsInstanceOfType(currentExecutor))
                    .Value;
                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor));
            }
        }

        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                kvp.Value.SetActive(false);
            }
        }
    }
}