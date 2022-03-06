using System.Threading;
using System.Threading.Tasks;
using Aivagames.Strategy.Core;
using Code.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Aivagames.Strategy.Abstractions
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;
        
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            var pointFrom = command.From;
            var pointTo = command.To;
            while (true)
            {
                GetComponent<NavMeshAgent>().destination = pointTo;
                _animator.SetTrigger(Walk);
                _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
                try
                {
                    await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
                }
                catch
                {
                    GetComponent<NavMeshAgent>().isStopped = true;
                    GetComponent<NavMeshAgent>().ResetPath();
                    break;
                }

                (pointFrom, pointTo) = (pointTo, pointFrom);
            }

            _stopCommandExecutor.CancellationTokenSource = null;
            _animator.SetTrigger(Idle);
        }
    }
}