using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Publisher.Configuration;
using Publisher.Model;

namespace Publisher.Services;

public class OrderDataService : IOrderDataService
{
    private readonly IMongoCollection<Order> _ordersCollection;

    public OrderDataService(
        IOptions<DatabaseSettings> projectSettings)
    {
        var mongoClient = new MongoClient(
            projectSettings.Value.MongoDbConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            projectSettings.Value.MongoDbDatabaseName);

        _ordersCollection = mongoDatabase.GetCollection<Order>(
            projectSettings.Value.MongoDbCollectionName);
    }

    public async Task<List<Order>> GetAsync() =>
        await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<Order?> GetAsync(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Order newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);

    public async Task UpdateAsync(string id, Order updatedOrder) =>
        await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

    public async Task RemoveAsync(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.Id == id);
}