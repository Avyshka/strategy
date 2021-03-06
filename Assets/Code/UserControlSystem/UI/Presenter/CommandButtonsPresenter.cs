using System;
using System.Collections.Generic;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model;
using Aivagames.Strategy.UserControlSystem.UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private CommandButtonsView _view;

        [Inject] private IObservable<ISelectable> _selectableValue;
        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectableValue.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }

            if (_currentSelectable != null)
            {
                _model.OnSelectionChange();
            }

            _currentSelectable = selectable;

            _view.Clear();

            if (selectable == null)
            {
                return;
            }

            var commandExecutors = new List<ICommandExecutor>();
            commandExecutors.AddRange((selectable as Component)
                                      ?.GetComponentsInParent<ICommandExecutor>()
                                      ?? Array.Empty<ICommandExecutor>());
            var queue = (selectable as Component)?.GetComponentInParent<ICommandsQueue>();
            _view.MakeLayout(commandExecutors, queue);
        }
    }
}