using System.ComponentModel.DataAnnotations;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreateApplicationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
