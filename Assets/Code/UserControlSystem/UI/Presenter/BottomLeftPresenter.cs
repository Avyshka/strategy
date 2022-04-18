using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class BottomLeftPresenter : MonoBehaviour
    {
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private Image _sliderFill;
        [SerializeField] private SelectableValue _selectableValue;

        private void Start()
        {
            _selectableValue.OnSelected += OnSelect;
            OnSelect(_selectableValue.CurrentValue);
        }

        private void OnSelect(ISelectable selected)
        {
            _selectedImage.enabled = selected != null;
            _healthSlider.gameObject.SetActive(selected != null);
            _text.enabled = selected != null;

            if (selected == null)
            {
                return;
            }
            
            _selectedImage.sprite = selected.Icon;
            _text.text = $"{selected.Health} / {selected.MaxHealth}";
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = selected.MaxHealth;
            _healthSlider.value = selected.Health;
            var color = Color.Lerp(Color.red, Color.green, selected.Health / selected.MaxHealth);
            _sliderBackground.color = color * 0.5f;
            _sliderFill.color = color;
        }
    }
}