namespace Mail.Contracts
{
    public class MailModel
    {
        public string From { get; init; } = null!;
        public string Recipient { get; init; } = null!;
        public string Subject { get; init; } = null!;
        public string Body { get; init; } = null!;
    }
}