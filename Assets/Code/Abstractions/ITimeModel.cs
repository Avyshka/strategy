using System;

namespace Aivagames.Strategy.Abstractions
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}