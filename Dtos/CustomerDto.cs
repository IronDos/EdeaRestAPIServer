using System;

namespace Edea.Dtos
{
    public record CustomerDto
    {
      public Guid Id { get; init; }
      public string CompanyName { get; set; }
    }
}