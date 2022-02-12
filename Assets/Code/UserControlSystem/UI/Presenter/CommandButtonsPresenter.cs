using System;
using System.Collections.Generic;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model;
using Aivagames.Strategy.UserControlSystem.UI.View;
using Aivagames.Strategy.Utils.AssetsInjector;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectableValue;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectableValue.OnSelected += OnSelected;
            OnSelected(_selectableValue.CurrentValue);
            _view.OnClick += OnButtonClick;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (selectable == null || _currentSelectable == selectable)
            {
                return;
            }

            _currentSelectable = selectable;

            _view.Clear();

            var commandExecutors = new List<ICommandExecutor>();
            commandExecutors.AddRange((selectable as Component)
                                      ?.GetComponentsInParent<ICommandExecutor>()
                                      ?? Array.Empty<ICommandExecutor>());
            _view.MakeLayout(commandExecutors);
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null)
            {
                unitProducer.ExecuteCommand(_context.Inject(new ProduceUnitCommandHeir()));
                return;
            }
            var mover = commandExecutor as CommandExecutorBase<IMoveCommand>;
            if (mover != null)
            {
                mover.ExecuteCommand(new MoveCommand());
                return;
            }
            var stopper = commandExecutor as CommandExecutorBase<IStopCommand>;
            if (stopper != null)
            {
                stopper.ExecuteCommand(new StopCommand());
                return;
            }
            var attacker = commandExecutor as CommandExecutorBase<IAttackCommand>;
            if (attacker != null)
            {
                attacker.ExecuteCommand(new AttackCommand());
                return;
            }
            var patroller = commandExecutor as CommandExecutorBase<IPatrolCommand>;
            if (patroller != null)
            {
                patroller.ExecuteCommand(new PatrolCommand());
                return;
            }
            throw new ApplicationException(
                $"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)}: Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
            
        }
    }
}