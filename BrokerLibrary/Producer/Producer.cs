using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace BrokerLibrary.Producer
{
    public class Producer : BaseClient, IProducer
    {
        private readonly ILogger<Producer> _logger;

        public Producer(ILogger<Producer> logger, IOptions<BrokerOptions> options) : base(logger, options)
        {
            _logger = logger;
        }

        public void Publish<T>(T message)
        {
            byte[] body;

            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(message);
                body = Encoding.UTF8.GetBytes(json);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Message serialization error");
                return;
            }

            Channel?.BasicPublish(exchange: string.Empty,
                                  routingKey: QueueName,
                                  basicProperties: null,
                                  body: body);

            _logger.LogInformation("Message published at: {time}", DateTime.UtcNow);
        }
    }
}
