using Consumer.Services;
using Consumer.Services.Order;
using MassTransit;

namespace Consumer.Configuration.Queue;

class OrderReceivingConsumerDefinition :
    ConsumerDefinition<OrderReceivedConsumerService>
{
    public OrderReceivingConsumerDefinition()
    {
        EndpointName = "order-receiving-queue";

        ConcurrentMessageLimit = 3;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OrderReceivedConsumerService> consumerConfigurator)
    {
        // configure message retry with millisecond intervals
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

        // use the outbox to prevent duplicate events from being published
        endpointConfigurator.UseInMemoryOutbox();

        endpointConfigurator.PrefetchCount = 10;
    }
}