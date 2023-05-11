using RabbitMQ.Client;

namespace BrokerLibrary
{
    public class BrokerOptions
    {
        public const string Name = "Broker";

        public string QueueName { get; init; } = null!;
        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 5672;
        public string UserName { get; init; } = ConnectionFactory.DefaultUser;
        public string Password { get; init; } = ConnectionFactory.DefaultPass;
    }
}
