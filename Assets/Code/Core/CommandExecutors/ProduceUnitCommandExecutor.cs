using Aivagames.Strategy.Core;
using UniRx;
using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [SerializeField] private Transform _unitParent;
        [SerializeField] private int _maxUnitsInQueue = 6;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        private void Update()
        {
            if (_queue.Count == 0)
            {
                return;
            }

            var innerTask = (UnitProductionTask) _queue[0];
            innerTask.TimeLeft += Time.deltaTime;
            if (innerTask.TimeLeft >= innerTask.ProductionTime)
            {
                RemoveTaskAtIndex(0);
                Instantiate(
                    innerTask.UnitPrefab,
                    new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                    Quaternion.identity,
                    _unitParent
                );
            }
        }

        public void Cancel(int index) => RemoveTaskAtIndex(index);

        private void RemoveTaskAtIndex(int index)
        {
            for (var i = index; i < _queue.Count - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }

            _queue.RemoveAt(_queue.Count - 1);
        }

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(
                command.ProductionTime,
                command.Icon,
                command.UnitPrefab,
                command.UnitName
            ));
        }
    }
}