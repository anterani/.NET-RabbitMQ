using BrokerLibrary;
using BrokerLibrary.Consumer;
using Mail.Consumer;
using Mail.Consumer.Configuration;
using Mail.Consumer.Services.Consumer;
using Mail.Consumer.Services.Mail;
using Mail.Contracts;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddHostedService<Worker>();

        services.Configure<MailOptions>(host.Configuration.GetSection(MailOptions.Name));
        services.Configure<BrokerOptions>(host.Configuration.GetSection(BrokerOptions.Name));

        services.AddScoped<IMailService, MailService>();
        services.AddSingleton<IConsumer<MailModel>, ConsumerService>();
    })
    .Build();

await host.RunAsync();
