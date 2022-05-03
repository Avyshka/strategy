using System;
using Aivagames.Strategy.Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Aivagames.Strategy.Core
{
    public class TimeModel : ITimeModel, ITickable
    {
        private readonly ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        public IObservable<int> GameTime => _gameTime.Select(f => (int) f);

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }
    }
}