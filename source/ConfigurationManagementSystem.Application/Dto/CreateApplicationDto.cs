using System.ComponentModel.DataAnnotations;

namespace ConfigurationManagementSystem.Application.Dto;

public class CreateApplicationDto
{
    [Required]
    public string Name { get; set; }
}
