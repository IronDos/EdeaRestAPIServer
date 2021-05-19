using System;

namespace Edea.Entities
{
    public record Customer
    {
      public Guid Id { get; init; }
      public string CompanyName { get; set; }
    }
}