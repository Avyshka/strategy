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
        public Action<ICommandExecutor, ICommandsQueue> OnClick;

        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _produceUnitButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>
            {
                {typeof(ICommandExecutor<IMoveCommand>), _moveButton},
                {typeof(ICommandExecutor<IStopCommand>), _stopButton},
                {typeof(ICommandExecutor<IAttackCommand>), _attackButton},
                {typeof(ICommandExecutor<IPatrolCommand>), _patrolButton},
                {typeof(ICommandExecutor<IProduceUnitCommand>), _produceUnitButton}
            };
        }

        public void BlockInteractions(ICommandExecutor commandExecutor)
        {
            UnblockAllInteractions();
            GetButtonGameObjectByType(commandExecutor.GetType())
                .GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllInteractions() => SetInteractable(true);

        private void SetInteractable(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _produceUnitButton.GetComponent<Selectable>().interactable = value;
        }

        public void MakeLayout(IEnumerable<ICommandExecutor> commandsExecutors, ICommandsQueue queue)
        {
            foreach (var currentExecutor in commandsExecutors)
            {
                var buttonGameObject = _buttonsByExecutorType
                    .First(type => type.Key.IsInstanceOfType(currentExecutor))
                    .Value;
                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, queue));
            }
        }

        private GameObject GetButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsByExecutorType
                .First(type => type.Key.IsAssignableFrom(executorInstanceType))
                .Value;
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