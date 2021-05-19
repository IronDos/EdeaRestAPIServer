using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Edea.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Edea.Repositories
{
  public class MongoDbOrdersRepository : IOrdersRepository
  {
    private const string databaseName = "Edea";
    private const string collectionName = "Orders";
    private readonly IMongoCollection<Order> ordersCollection;
    private readonly FilterDefinitionBuilder<Order> filterBuilder = Builders<Order>.Filter;

    public MongoDbOrdersRepository(IMongoClient mongoClient) {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      ordersCollection = database.GetCollection<Order>(collectionName);
    }
    public async Task CreateOrderAsync(Order order)
    {
      await ordersCollection.InsertOneAsync(order);
    }

    public async Task DeleteOrderAsync(Guid id)
    {
      var filter = filterBuilder.Eq(order => order.Id, id);
      await ordersCollection.DeleteOneAsync(filter);
    }

    public async Task<Order> GetOrderAsync(Guid id)
    {
      var filter = filterBuilder.Eq(order => order.Id, id);
      return await ordersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
      return await ordersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
      var filter = filterBuilder.Eq(existingOrder => existingOrder.Id, order.Id);
      await ordersCollection.ReplaceOneAsync(filter, order);
    }
  }
}