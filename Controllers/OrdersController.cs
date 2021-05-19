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
  [ApiController]
  [Route("orders")]
  public class OrdersController : ControllerBase
  {
    private readonly IOrdersRepository repository;
    public OrdersController(IOrdersRepository repository) {
      // Data
      this.repository = repository;
    }

    // GET /orders
    [HttpGet]
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync() {
      var orders = (await repository.GetOrdersAsync())
                    .Select(order => order.AsDto());
      return orders;
    }

    // GET /orders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderAsync(Guid id) {
      var order = await repository.GetOrderAsync(id);
      if (order is null) {
        return NotFound();
      }
      return order.AsDto();
    }

    // POST /orders
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrderAsync(CreateOrderDto orderDto) {
      Order order = new() {
        Id = Guid.NewGuid(),
        CustomerId = orderDto.CustomerId,
        Address = orderDto.Address,
        City = orderDto.City,
        Region = orderDto.Region,
        ShippingCost = orderDto.ShippingCost
      };

      await repository.CreateOrderAsync(order);

      return CreatedAtAction(nameof(GetOrderAsync), new { id = order.Id }, order.AsDto());
    }

    // PUT /orders
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderAsync(Guid id, UpdateOrderDto orderDto) {
      var existingOrder = await repository.GetOrderAsync(id);
      
      if (existingOrder is null) {
        return NotFound();
      }

      Order updatedOrder = existingOrder with {
        CustomerId = orderDto.CustomerId,
        Address = orderDto.Address,
        City = orderDto.City,
        Region = orderDto.Region,
        ShippingCost = orderDto.ShippingCost
      };

      await repository.UpdateOrderAsync(updatedOrder);
      return NoContent();
    }

    // DELETE /orders/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrderAsync(Guid id) {
      var existingOrder = await repository.GetOrderAsync(id);
      
      if (existingOrder is null) {
        return NotFound();
      }

      await repository.DeleteOrderAsync(id);
      
      return NoContent();
    }


  }
}