using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _backButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    gameObject.SetActive(false);
                    Time.timeScale = 1f;
                });

            _exitButton
                .OnClickAsObservable()
                .Subscribe(_ => Application.Quit());
        }
    }
}