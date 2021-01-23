using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class CreateEnvDto
    {
        public Guid Project { get; set; }

        [Required]
        public string Env { get; set; }
    }
}
