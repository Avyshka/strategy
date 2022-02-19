using System;
using Code.Utils;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
    {
        private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;
        private TAwaited _result;
        private Action _continuation;
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;
        public TAwaited GetResult() => _result;

        public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
        {
            _scriptableObjectValueBase = scriptableObjectValueBase;
            _scriptableObjectValueBase.OnNewValue += OnNewValue;
        }

        private void OnNewValue(TAwaited obj)
        {
            _scriptableObjectValueBase.OnNewValue -= OnNewValue;
            _result = obj;
            _isCompleted = true;
            _continuation?.Invoke();
        }

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }
    }
}