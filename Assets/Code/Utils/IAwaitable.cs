namespace Code.Utils
{
    public interface IAwaitable<T>
    {
        IAwaiter<T> GetAwaiter();
    }
}