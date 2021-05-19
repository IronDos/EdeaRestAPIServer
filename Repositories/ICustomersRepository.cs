using System;
using Edea.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edea.Repositories
{
  public interface ICustomersRepository
  {
    Task<Customer> GetCustomerAsync(Guid id);
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task CreateCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Guid id);
  }
}