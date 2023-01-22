using Publisher.Model;

namespace Publisher.Services;

public interface IOrderService
{
    Task<Order?> GetOrder(string id);
    Task CreateOrder(Order order);
}