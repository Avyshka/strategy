using Aivagames.Strategy.Abstractions;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class AttackerParallelInfoUpdater : MonoBehaviour, ITickable
    {
        [Inject] private IAutomaticAttacker _automaticAttacker;
        [Inject] private ICommandsQueue _queue;

        public void Tick()
        {
            AutoAttackEvaluator.AttackersInfo.AddOrUpdate(
                gameObject,
                new AutoAttackEvaluator.AttackerParallelInfo(_automaticAttacker.VisionRadius, _queue.CurrentCommand),
                (go, value) =>
                {
                    value.VisionRadius = _automaticAttacker.VisionRadius;
                    value.CurrentCommand = _queue.CurrentCommand;
                    return value;
                }
            );
        }

        private void OnDestroy()
        {
            AutoAttackEvaluator.AttackersInfo.TryRemove(gameObject, out _);
        }
    }
}