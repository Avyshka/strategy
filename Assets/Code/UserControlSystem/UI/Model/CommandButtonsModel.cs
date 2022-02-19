using System;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model.CommandCreators;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    public class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;

        private bool _commandIsPending;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
        {
            if (_commandIsPending)
            {
                ProcessOnCancel();
            }

            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor,
                command => executeCommandWrapper(commandExecutor, command));
            _attacker.ProcessCommandExecutor(commandExecutor,
                command => executeCommandWrapper(commandExecutor, command));
            _mover.ProcessCommandExecutor(commandExecutor,
                command => executeCommandWrapper(commandExecutor, command));
            _stopper.ProcessCommandExecutor(commandExecutor,
                command => executeCommandWrapper(commandExecutor, command));
            _patroller.ProcessCommandExecutor(commandExecutor,
                command => executeCommandWrapper(commandExecutor, command));
        }

        public void executeCommandWrapper(ICommandExecutor commandExecutor, object command)
        {
            commandExecutor.ExecuteCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChange()
        {
            _commandIsPending = false;
            ProcessOnCancel();
        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _mover.ProcessCancel();
            _stopper.ProcessCancel();
            _patroller.ProcessCancel();
            
            OnCommandCancel?.Invoke();
        }
    }
}