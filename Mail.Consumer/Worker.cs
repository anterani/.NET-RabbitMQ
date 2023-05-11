using BrokerLibrary.Consumer;
using Mail.Contracts;

namespace Mail.Consumer
{
    public class Worker : BackgroundService
    {
        public Worker(IConsumer<MailModel> consumer)
        {
            consumer.Listen();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
    }
}