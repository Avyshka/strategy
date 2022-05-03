using Aivagames.Strategy.Abstractions;

namespace Aivagames.Strategy.UserControlSystem
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}