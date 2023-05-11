using Mail.Contracts;

namespace Mail.Consumer.Services.Mail
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailModel mail);
    }
}
