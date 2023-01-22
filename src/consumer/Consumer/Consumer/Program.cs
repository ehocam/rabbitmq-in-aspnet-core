using Consumer.Configuration;
using Consumer.Configuration.Queue;
using Consumer.Services;
using Consumer.Services.Order;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var settings = config.GetRequiredSection("Settings").Get<ProjectSettings>();

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(settings.Host, settings.VirtualHost, h =>
                {
                    h.Username(settings.UserName);
                    h.Password(settings.Password);
                });

                cfg.MessageTopology.SetEntityNameFormatter(new ConsumerEnvironmentSetup(""));
                
                cfg.ConfigureEndpoints(context);
            });

            x.AddConsumer<OrderReceivedConsumerService>();
            x.AddConsumer<OrderPickedUpConsumerService>();


            x.SetKebabCaseEndpointNameFormatter();
        });
    })
    .Build()
    .Run();