using Consumer.Services;
using Consumer.Services.Order;
using MassTransit;

namespace Consumer.Configuration.Queue;

public class OrderPickedUpConsumerDefinition :
    ConsumerDefinition<OrderReceivedConsumerService>
{
    public OrderPickedUpConsumerDefinition()
    {
        EndpointName = "order-picked_up-queue";

        ConcurrentMessageLimit = 8;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OrderReceivedConsumerService> consumerConfigurator)
    {
        endpointConfigurator.PrefetchCount = 5;
    }
}