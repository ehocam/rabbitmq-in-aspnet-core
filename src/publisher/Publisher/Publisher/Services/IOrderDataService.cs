using Publisher.Model;

namespace Publisher.Services;

public interface IOrderDataService
{
    Task<List<Order>> GetAsync();
    Task<Order?> GetAsync(string id);
    Task CreateAsync(Order newOrder);
    Task UpdateAsync(string id, Order updatedOrder);
    Task RemoveAsync(string id);
}