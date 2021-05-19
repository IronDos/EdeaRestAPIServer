using Edea.Entities;
using Edea.Dtos;

namespace Edea
{
    public static class Extensions {
      public static CustomerDto AsDto(this Customer customer) {
        return new CustomerDto{
          Id = customer.Id,
          CompanyName = customer.CompanyName
        };
      }

      public static OrderDto AsDto(this Order order) {
        return new OrderDto{
          Id = order.Id,
          CustomerId = order.CustomerId,
          Address = order.Address,
          City = order.City,
          Region = order.Region,
          ShippingCost = order.ShippingCost
        };
      }
    }
}