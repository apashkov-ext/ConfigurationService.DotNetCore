using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }
    }
}
