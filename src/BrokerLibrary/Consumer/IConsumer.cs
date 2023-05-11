namespace BrokerLibrary.Consumer
{
    public interface IConsumer<T>
    {
        void Listen();
        Task<bool> ProceedAsync(T message);
    }
}
