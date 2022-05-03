using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Aivagames.Strategy.Abstractions
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public class AttackOperation : IAwaitable<AsyncExtensions.Void>
        {
            public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {
                private readonly AttackOperation _attackOperation;

                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    attackOperation.OnComplete += OnComplete;
                }

                private void OnComplete()
                {
                    _attackOperation.OnComplete -= OnComplete;
                    OnWaitFinish(new AsyncExtensions.Void());
                }
            }

            private event Action OnComplete;

            private readonly AttackCommandExecutor _attackCommandExecutor;
            private readonly IAttackable _target;

            private bool _isCanceled;

            public AttackOperation(AttackCommandExecutor attackCommandExecutor, IAttackable target)
            {
                _attackCommandExecutor = attackCommandExecutor;
                _target = target;

                var thread = new Thread(AttackAlgorithm);
                thread.Start();
            }

            public void Cancel()
            {
                _isCanceled = true;
                OnComplete?.Invoke();
            }

            private void AttackAlgorithm(object obj)
            {
                while (true)
                {
                    if (
                        _attackCommandExecutor == null ||
                        _attackCommandExecutor._ourHealth.Health == 0 ||
                        _target.Health == 0 ||
                        _isCanceled
                    )
                    {
                        OnComplete?.Invoke();
                        return;
                    }

                    var targetPosition = default(Vector3);
                    var ourPosition = default(Vector3);
                    var ourRotation = default(Quaternion);
                    lock (_attackCommandExecutor)
                    {
                        targetPosition = _attackCommandExecutor._targetPosition;
                        ourPosition = _attackCommandExecutor._ourPosition;
                        ourRotation = _attackCommandExecutor._ourRotation;
                    }

                    var vector = targetPosition - ourPosition;
                    var distanceToTarget = vector.magnitude;
                    if (distanceToTarget > _attackCommandExecutor._attackDistance)
                    {
                        var finalDestination = targetPosition -
                                               vector.normalized * (_attackCommandExecutor._attackDistance * 0.9f);
                        _attackCommandExecutor._targetPositions.OnNext(finalDestination);
                        Thread.Sleep(100);
                    }
                    else if (ourRotation != Quaternion.LookRotation(vector))
                    {
                        _attackCommandExecutor._targetRotations.OnNext(Quaternion.LookRotation(vector));
                    }
                    else
                    {
                        _attackCommandExecutor._attackTargets.OnNext(_target);
                        Thread.Sleep(_attackCommandExecutor._attackPeriod);
                    }
                }
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new AttackOperationAwaiter(this);
        }

        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        [Inject] private IHealthHolder _ourHealth;
        [Inject(Id = "AttackDistance")] private float _attackDistance;
        [Inject(Id = "AttackPeriod")] private int _attackPeriod;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOperation;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Idle = Animator.StringToHash("Idle");

        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value => new Vector3((float) Math.Round(value.x, 2), (float) Math.Round(value.y, 2),
                    (float) Math.Round(value.z, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(StartMovingToPosition);

            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(StartAttackingTargets);

            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(SetAttackRotation);
        }

        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
            _animator.SetTrigger(Attack);
            target.ReceiveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;
            _animator.SetTrigger(Walk);
        }

        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            _targetTransform = (command.Target as Component).transform;
            _currentAttackOperation = new AttackOperation(this, command.Target);
            Update();
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _currentAttackOperation.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                _currentAttackOperation.Cancel();
            }

            _animator.SetTrigger(Idle);
            _currentAttackOperation = null;
            _targetTransform = null;
            _stopCommandExecutor.CancellationTokenSource = null;
        }

        private void Update()
        {
            if (_currentAttackOperation == null)
            {
                return;
            }

            lock (this)
            {
                _ourPosition = transform.position;
                _ourRotation = transform.rotation;
                if (_targetTransform != null)
                {
                    _targetPosition = _targetTransform.position;
                }
            }
        }
    }
}