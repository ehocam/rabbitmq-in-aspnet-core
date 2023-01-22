using Consumer.Events.Order;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer.Services.Order;

public class OrderPickedUpConsumerService : IConsumer<OrderPickedUp>
{
    private readonly ILogger<OrderPickedUpConsumerService> _logger;

    public OrderPickedUpConsumerService(ILogger<OrderPickedUpConsumerService> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderPickedUp> context)
    {
        _logger.LogInformation("Order OrderPickedUpConsumerService: {OrderId}", context.Message.OrderId);
        
        
        
        return Task.CompletedTask;
    }
}