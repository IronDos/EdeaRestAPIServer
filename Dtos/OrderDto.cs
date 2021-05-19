using System;

namespace Edea.Entities
{
    public record OrderDto
    {
      public Guid Id { get; init; }
      public string CustomerId { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string Region { get; set; }
      public double ShippingCost { get; set; }
    }
}