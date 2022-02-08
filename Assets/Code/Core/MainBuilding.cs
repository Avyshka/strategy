using UnityEngine;

namespace Aivagames.Strategy.Core
{
    public class MainBuilding : MonoBehaviour
    {
        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitsParent;

        public void ProduceUnit()
        {
            Instantiate(
                _unitPrefab,
                new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                Quaternion.identity,
                _unitsParent
            );
        }
    }
}