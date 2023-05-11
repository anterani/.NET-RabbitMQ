using BrokerLibrary;
using BrokerLibrary.Producer;
using Mail.Producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddHostedService<Worker>();
        services.Configure<BrokerOptions>(host.Configuration.GetSection(BrokerOptions.Name));
        services.AddSingleton<IProducer, Producer>();
    })
    .Build();

await host.RunAsync();
