using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BrokerLibrary
{
    public abstract class BaseClient : IDisposable
    {
        private readonly ILogger _logger;
        private readonly IConnection? _connection;
        
        protected IModel? Channel { get; private set; }
        protected string QueueName { get; private set; }

        public BaseClient(ILogger<BaseClient> logger, IOptions<BrokerOptions> options, bool dispatchConsumersAsync = false)
        {
            _logger = logger;
            QueueName = options.Value.QueueName;

            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = options.Value.Host, 
                    Port = options.Value.Port,
                    UserName = options.Value.UserName,
                    Password = options.Value.Password,
                    DispatchConsumersAsync = dispatchConsumersAsync
                };

                _connection = factory.CreateConnection();
            }
            catch(BrokerUnreachableException ex)
            {
                _logger.LogError(ex, "Connection failed");
                return;
            }

            Channel = _connection.CreateModel();
            Channel.QueueDeclare(queue: QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void Dispose()
        {
            Channel?.Close();
            Channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}