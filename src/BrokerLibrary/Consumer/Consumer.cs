using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BrokerLibrary.Consumer
{
    public abstract class Consumer<T> : BaseClient, IConsumer<T>
    {
        private readonly ILogger<Consumer<T>> _logger;

        public Consumer(ILogger<Consumer<T>> logger, IOptions<BrokerOptions> options) : base(logger, options, true)
        {
            _logger = logger;
        }

        public void Listen()
        {
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.Received += Receive;

            Channel?.BasicConsume(QueueName, false, consumer);
        }

        private async Task Receive(object sender, BasicDeliverEventArgs ea)
        {
            T? message = default;

            try
            {
                var body = ea.Body.ToArray();
                var json = System.Text.Encoding.UTF8.GetString(body);
                message = System.Text.Json.JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Message deserialization error");
            }

            if(message is not null)
            {
                if (await ProceedAsync(message))
                {
                    Channel?.BasicAck(ea.DeliveryTag, false);
                } 
                else
                {
                    _logger.LogError("Message proceed failed");
                }
            }
            
            await Task.Yield();
        }

        public abstract Task<bool> ProceedAsync(T message);
    }
}
