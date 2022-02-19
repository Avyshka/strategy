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

        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private Transform _groundTransform;

        private Plane _groundPlane;

        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButtonUp(1))
            {
                return;
            }

            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
            {
                var hits = Physics.RaycastAll(ray);
                if (hits.Length == 0)
                {
                    return;
                }

                var selectable = hits
                    .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                    .FirstOrDefault(c => c != null);
                _selectableValue?.SetValue(selectable);
            }
            else
            {
                if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            }
        }
    }
}