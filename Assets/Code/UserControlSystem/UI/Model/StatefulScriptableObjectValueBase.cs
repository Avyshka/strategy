using System;
using Aivagames.Strategy.UserControlSystem.UI.Model;
using UniRx;

namespace Code.Utils
{
    public abstract class StatefulScriptableObjectValueBase<T> : ScriptableObjectValueBase<T>, IObservable<T>
    {
        private ReactiveProperty<T> _innerDataSource = new ReactiveProperty<T>();

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _innerDataSource.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer) => _innerDataSource.Subscribe(observer);
    }
}