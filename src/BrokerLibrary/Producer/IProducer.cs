namespace BrokerLibrary.Producer
{
    public interface IProducer
    {
        void Publish<T>(T message);
    }
}
