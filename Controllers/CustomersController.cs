using System.Threading;
using System;
using Microsoft.AspNetCore.Mvc;
using Edea.Repositories;
using System.Collections.Generic;
using System.Linq;
using Edea.Entities;
using Edea.Dtos;
using System.Threading.Tasks;

namespace Edea.Controllers
{
  // Get /customers
  [ApiController]
  [Route("customers")]
  public class CustomersController : ControllerBase
  {
    private readonly ICustomersRepository repository;
    public CustomersController(ICustomersRepository repository) {
      // Data
      this.repository = repository;
    }

    // GET /customers
    [HttpGet]
    public async Task<IEnumerable<CustomerDto>> GetCustomersAsync() {
      var customers = (await repository.GetCustomersAsync())
                      .Select(customer => customer.AsDto());
      return customers;
    }

    // GET /customers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerAsync(Guid id) {
      var customer = await repository.GetCustomerAsync(id);
      if (customer is null) {
        return NotFound();
      }
      return customer.AsDto();
    }

    // POST /customers
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomerAsync(CreateCustomerDto customerDto) {
      Customer customer = new() {
        Id = Guid.NewGuid(),
        CompanyName = customerDto.CompanyName
      };

      await repository.CreateCustomerAsync(customer);

      return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer.AsDto());
    }

    // PUT /customers
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomerAsync(Guid id, UpdateCustomerDto customerDto) {
      var existingCustomer = await repository.GetCustomerAsync(id);
      
      if (existingCustomer is null) {
        return NotFound();
      }

      Customer updatedCustomer = existingCustomer with {
        CompanyName = customerDto.CompanyName
      };

      await repository.UpdateCustomerAsync(updatedCustomer);
      return NoContent();
    }

    // DELETE /customers/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustoemrAsync(Guid id) {
      var existingCustomer = await repository.GetCustomerAsync(id);
      
      if (existingCustomer is null) {
        return NotFound();
      }

      await repository.DeleteCustomerAsync(id);
      
      return NoContent();
    }
  }
}