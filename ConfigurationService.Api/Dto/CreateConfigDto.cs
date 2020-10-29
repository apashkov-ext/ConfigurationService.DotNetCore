using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class CreateConfigDto
    {
        [Required]
        public string Env { get; set; }
    }
}
