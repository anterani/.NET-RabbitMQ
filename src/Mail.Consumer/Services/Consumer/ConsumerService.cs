using BrokerLibrary;
using BrokerLibrary.Consumer;
using Mail.Consumer.Services.Mail;
using Mail.Contracts;
using Microsoft.Extensions.Options;

namespace Mail.Consumer.Services.Consumer
{
    public class ConsumerService : Consumer<MailModel>
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsumerService(ILogger<ConsumerService> logger, IOptions<BrokerOptions> options, IServiceProvider serviceProvider) : base(logger, options)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ProceedAsync(MailModel message)
        {
            using(var scope = _serviceProvider.CreateScope()) 
            {
                IMailService mailService = scope.ServiceProvider.GetRequiredService<IMailService>();
                if(await mailService.SendAsync(message))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
