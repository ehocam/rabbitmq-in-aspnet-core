using MassTransit;
using Publisher.Messages;
using Publisher.Model;

namespace Publisher.Services;

public class OrderService : IOrderService
{
    private readonly IOrderDataService _orderDataService;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderService(IOrderDataService orderDataService, IPublishEndpoint publishEndpoint)
    {
        _orderDataService = orderDataService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Order?> GetOrder(string id) =>
        await _orderDataService.GetAsync(id);

    public async Task CreateOrder(Order order)
    {
        await _orderDataService.CreateAsync(order);
        await _publishEndpoint.Publish(new OrderMessage
        {
            Id = order.Id
        });
    }
}