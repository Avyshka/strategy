using System;

namespace Aivagames.Strategy.Abstractions
{
    public interface IGameStatus
    {
        IObservable<int> Status { get; }
    }
}