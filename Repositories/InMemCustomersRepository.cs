using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Edea.Entities;

namespace Edea.Repositories
{
  

  class InMemCustomersRepository : ICustomersRepository
  {
    private readonly List<Customer> customers = new()
    {
      new Customer { Id = Guid.NewGuid(), CompanyName = "Asaf Company" },
      new Customer { Id = Guid.NewGuid(), CompanyName = "Or Company" },
      new Customer { Id = Guid.NewGuid(), CompanyName = "Galit Company" },
      new Customer { Id = Guid.NewGuid(), CompanyName = "Nissim Company" }
    };

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
      return await Task.FromResult(customers);
    }

    public async Task<Customer> GetCustomerAsync(Guid id)
    {
      var customer = customers.Where(customer => customer.Id == id).SingleOrDefault();
      return await Task.FromResult(customer);
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
      customers.Add(customer);
      await Task.CompletedTask;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
      var index = customers.FindIndex(existingCustomer => existingCustomer.Id == customer.Id);
      customers[index] = customer;
      await Task.CompletedTask;
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
      var index = customers.FindIndex(existingCustomer => existingCustomer.Id == id);
      customers.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
}