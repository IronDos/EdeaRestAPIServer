using System.ComponentModel.DataAnnotations;
namespace Edea.Dtos
{
  public record CreateCustomerDto
  {
    [Required]
    [MaxLength(30)]
    public string CompanyName { get; set; }
  }
}