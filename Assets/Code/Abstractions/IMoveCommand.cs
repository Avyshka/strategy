using UnityEngine;

namespace Aivagames.Strategy.Abstractions
{
    public interface IMoveCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}