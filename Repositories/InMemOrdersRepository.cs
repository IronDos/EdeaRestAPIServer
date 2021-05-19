using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Edea.Entities;

namespace Edea.Repositories
{
  class InMemOrdersRepository : IOrdersRepository
  {
    private readonly List<Order> orders = new()
    {
      new Order { Id = Guid.NewGuid(), Address = "a10", City = "Tel Aviv", Region = "Israel", ShippingCost = 10.1 },
      new Order { Id = Guid.NewGuid(), Address = "b20", City = "Rome", Region = "Italy", ShippingCost = 20.1 },
      new Order { Id = Guid.NewGuid(), Address = "c30", City = "London", Region = "UK", ShippingCost = 30.1 },
      new Order { Id = Guid.NewGuid(), Address = "d40", City = "Moscow", Region = "Russia", ShippingCost = 100.1 },
    };

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
      return await Task.FromResult(orders);
    }

    public async Task<Order> GetOrderAsync(Guid id)
    {
      var order = orders.Where(order => order.Id == id).SingleOrDefault();
      return await Task.FromResult(order);
    }

    public async Task CreateOrderAsync(Order order)
    {
      orders.Add(order);
      await Task.CompletedTask;
    }

    public async Task UpdateOrderAsync(Order order)
    {
      var index = orders.FindIndex(existingOrder => existingOrder.Id == order.Id);
      orders[index] = order;
      await Task.CompletedTask;
    }

    public async Task DeleteOrderAsync(Guid id)
    {
      var index = orders.FindIndex(existingOrder => existingOrder.Id == id);
      orders.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
}