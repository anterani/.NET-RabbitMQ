namespace Mail.Consumer.Configuration
{
    public class MailOptions
    {
        public const string Name = "Mail";

        public string Host { get; init; } = "localhost";
        public int Port { get; init; } = 587;
    }
}
