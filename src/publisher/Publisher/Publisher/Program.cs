using MassTransit;
using Microsoft.OpenApi.Models;
using Publisher.Configuration;
using Publisher.Constants;
using Publisher.Messages;
using Publisher.Services;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
// Configuration
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
// Project Settings Initialization
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(ProjectDefaultConstant.ProjectSettings));
builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection(ProjectDefaultConstant.MessageBrokerSettings));
// Add services to the container.
builder.Services.AddSingleton<OrderDataService>();
// MassTransit Configuration
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, configuration) =>
    {
        var settings = config.GetRequiredSection(ProjectDefaultConstant.MessageBrokerSettings)
            .Get<MessageBrokerSettings>();

        configuration.Host(settings.RabbitMqHost, hostConfigurator =>
        {
            hostConfigurator.Username(settings.RabbitMqUserName);
            hostConfigurator.Password(settings.RabbitMqPassword);
        });

        configuration.Send<OrderMessage>(x => { x.UseRoutingKeyFormatter(context => "created"); });
        configuration.Message<OrderMessage>(x => { x.SetEntityName("order"); });
        configuration.Publish<OrderMessage>(x => { x.ExchangeType = ExchangeType.Direct; });
    });
});

// OPTIONAL, but can be used to configure the bus options
builder.Services.AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        // if specified, waits until the bus is started before
        // returning from IHostedService.StartAsync
        // default is false
        options.WaitUntilStarted = true;

        // if specified, limits the wait time when starting the bus
        options.StartTimeout = TimeSpan.FromSeconds(10);

        // if specified, limits the wait time when stopping the bus
        options.StopTimeout = TimeSpan.FromSeconds(30);
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order API",
        Description = "You can use it to create new order"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();