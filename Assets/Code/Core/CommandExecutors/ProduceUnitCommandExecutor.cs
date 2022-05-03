using System.Threading.Tasks;
using Aivagames.Strategy.Core;
using Aivagames.Strategy.UserControlSystem;
using Code.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.Abstractions
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        [Inject] private DiContainer _diContainer;

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
                var instance = _diContainer.InstantiatePrefab(
                    innerTask.UnitPrefab,
                    transform.position,
                    Quaternion.identity,
                    _unitParent
                );
                var factionMember = instance.GetComponent<FactionMember>();
                factionMember.SetFaction(GetComponent<FactionMember>().FactionId);
                var queue = instance.GetComponent<ICommandsQueue>();
                var mainBuilding = GetComponent<MainBuilding>();
                queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));
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

        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
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