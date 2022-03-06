using System.Text;
using Aivagames.Strategy.Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _gameStatus.Status
                .ObserveOnMainThread()
                .Subscribe(result =>
                {
                    var sb = new StringBuilder($"Game Over!");
                    if (result == 0)
                    {
                        sb.AppendLine("Tie!");
                    }
                    else
                    {
                        sb.AppendLine($"Faction #{result} won!");
                    }
                    _view.SetActive(true);
                    _text.text = sb.ToString();
                    Time.timeScale = 0;
                });
        }
    }
}