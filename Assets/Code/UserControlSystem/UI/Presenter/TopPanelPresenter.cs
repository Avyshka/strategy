using System;
using Aivagames.Strategy.Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class TopPanelPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _inputField;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menuGameObject;

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _inputField.text = $"{t.Minutes:D2}:{t.Seconds:D2}";
            });

            _menuButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _menuGameObject.SetActive(true);
                    Time.timeScale = 0f;
                });
        }
    }
}