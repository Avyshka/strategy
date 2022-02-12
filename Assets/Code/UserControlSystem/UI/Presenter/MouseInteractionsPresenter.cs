using System.Linq;
using Aivagames.Strategy.Abstractions;
using Aivagames.Strategy.UserControlSystem.UI.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Aivagames.Strategy.UserControlSystem.UI.Presenter
{
    public class MouseInteractionsPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectableValue;
        [SerializeField] private EventSystem _eventSystem;
        
        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) || _eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                return;
            }

            var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault(c => c != null);
            _selectableValue?.SetValue(selectable);
        }
    }
}