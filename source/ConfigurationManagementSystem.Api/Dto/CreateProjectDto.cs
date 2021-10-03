using System.ComponentModel.DataAnnotations;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }
    }
}
