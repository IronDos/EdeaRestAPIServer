using System;
using Edea.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edea.Repositories
{
  public interface IOrdersRepository
  {
    Task<Order> GetOrderAsync(Guid id);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Guid id);
  }
}