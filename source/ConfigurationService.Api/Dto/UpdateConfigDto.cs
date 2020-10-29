using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class UpdateConfigDto
    {
        [Required]
        public string Content { get; set; }
    }
}
