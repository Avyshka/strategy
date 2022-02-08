using System.Linq;
using Aivagames.Strategy.Abstractions;
using UnityEngine;

namespace Aivagames.Strategy.UserControlSystem
{
    public class MouseInteractionsHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
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
            _selectedObject?.SetValue(selectable);
        }
    }
}