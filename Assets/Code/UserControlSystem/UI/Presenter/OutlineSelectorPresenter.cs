using System;
using Aivagames.Strategy.Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class OutlineSelectorPresenter : MonoBehaviour
    {
        [Inject] private IObservable<ISelectable> _selectableValue;

        private const float UnSelectedOutlineWidth = 0f;
        private const float SelectedOutlineWidth = 5f;
        private readonly Color _selectedOutlineColor = new Color(0.65f, 1f, 0.75f, 0.55f);

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectableValue.Subscribe(OnSelect);
        }

        private void OnSelect(ISelectable selected)
        {
            ChangeOutlineWidth(UnSelectedOutlineWidth);
            if (selected == null)
            {
                return;
            }

            _currentSelectable = selected;
            ChangeOutlineWidth(SelectedOutlineWidth);
        }

        private void ChangeOutlineWidth(float width)
        {
            if (_currentSelectable == null)
            {
                return;
            }

            var component = _currentSelectable as Component;
            if (component != null && component.TryGetComponent<Outline>(out var outline))
            {
                outline.OutlineWidth = width;
                outline.OutlineColor = _selectedOutlineColor;
            }
        }
    }
}