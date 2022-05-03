namespace Aivagames.Strategy.Abstractions
{
    public interface ICommandExecutor
    {
    }

    public interface ICommandExecutor<T> : ICommandExecutor where T : ICommand
    {
    }
}