using Bogus;
using BrokerLibrary.Producer;
using Mail.Contracts;

namespace Mail.Producer
{
    public class Worker : BackgroundService
    {
        private readonly IProducer _producer;

        public Worker(IProducer producer)
        {
            _producer = producer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var faker = new Faker("pl");

            do
            {
                var message = new MailModel
                {
                    From = "support@example.com",
                    Recipient = faker.Internet.Email(),
                    Subject = faker.Lorem.Paragraph(1),
                    Body = faker.Lorem.Text(),
                };

                _producer.Publish(message);

                await Task.Delay(10000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}