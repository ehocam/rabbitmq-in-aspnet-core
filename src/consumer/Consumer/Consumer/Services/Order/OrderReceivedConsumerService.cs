using Consumer.Events.Order;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer.Services.Order;

public class OrderReceivedConsumerService : IConsumer<OrderReceived>
{
    private readonly ILogger<OrderReceivedConsumerService> _logger;

    public OrderReceivedConsumerService(ILogger<OrderReceivedConsumerService> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderReceived> context)
    {
        _logger.LogInformation("Order Submitted: {OrderId}", context.Message.OrderId);
        
        return Task.CompletedTask;
    }
}