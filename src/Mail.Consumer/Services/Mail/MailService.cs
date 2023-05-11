using Mail.Consumer.Configuration;
using Mail.Contracts;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Mail.Consumer.Services.Mail
{
    internal class MailService : IMailService
    {
        private readonly ILogger _logger;
        private readonly IOptions<MailOptions> _options;

        public MailService(ILogger<MailService> logger, IOptions<MailOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        public async Task<bool> SendAsync(MailModel mail)
        {
            try
            {
                using var client = new SmtpClient(_options.Value.Host, _options.Value.Port);
                await client.SendMailAsync(mail.From, mail.Recipient, mail.Subject, mail.Body);

                _logger.LogInformation("Mail sent at: {time}", DateTime.UtcNow);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mail sending error");
            }

            return false;
        }
    }
}
