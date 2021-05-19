using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Edea.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Edea.Repositories
{
  public class MongoDbCustomersRepository : ICustomersRepository
  {
    private const string databaseName = "Edea";
    private const string collectionName = "Customers";
    private readonly IMongoCollection<Customer> customersCollection;
    private readonly FilterDefinitionBuilder<Customer> filterBuilder = Builders<Customer>.Filter;
    public MongoDbCustomersRepository(IMongoClient mongoClient) {
      IMongoDatabase database = mongoClient.GetDatabase(databaseName);
      customersCollection = database.GetCollection<Customer>(collectionName);
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
      await customersCollection.InsertOneAsync(customer);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
      var filter = filterBuilder.Eq(customer => customer.Id, id);
      await customersCollection.DeleteOneAsync(filter);
    }

    public async Task<Customer> GetCustomerAsync(Guid id)
    {
      var filter = filterBuilder.Eq(customer => customer.Id, id);
      return await customersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
      return await customersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
      var filter = filterBuilder.Eq(existingCustomer => existingCustomer.Id, customer.Id);
      await customersCollection.ReplaceOneAsync(filter, customer);
    }
  }
}