using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface IPatrolCommand : ICommand
    {
        public Vector3 From { get; }
        public Vector3 To { get; }
    }
}