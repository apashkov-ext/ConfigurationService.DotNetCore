using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class CreateEnvDto
    {
        public Guid Project { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
