using Code.Utils;

namespace Aivagames.Strategy.UserControlSystem.UI.Model
{
    public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
    {
        private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;

        public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
        {
            _scriptableObjectValueBase = scriptableObjectValueBase;
            _scriptableObjectValueBase.OnNewValue += OnNewValue;
        }

        private void OnNewValue(TAwaited obj)
        {
            _scriptableObjectValueBase.OnNewValue -= OnNewValue;
            OnWaitFinish(obj);
        }
    }
}