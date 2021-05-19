using System;
using System.ComponentModel.DataAnnotations;

namespace Edea.Dtos
{
    public record CreateOrderDto
    {
      [Required]
      public string CustomerId { get; set; }
      
      [Required]
      public string Address { get; set; }
      
      [Required]
      public string City { get; set; }
      
      [Required]
      public string Region { get; set; }
      
      [Required]
      public double ShippingCost { get; set; }
    }
}