namespace Aivagames.Strategy.Abstractions
{
    public interface IAttackCommand : ICommand
    {
        public IAttackable Target { get; }
    }
}