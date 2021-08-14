using ConfigurationService.Api.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class CreateEnvDto
    {
        [ValidGuid]
        public Guid Project { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
