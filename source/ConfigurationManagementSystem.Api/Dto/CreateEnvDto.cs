using System;
using System.ComponentModel.DataAnnotations;
using ConfigurationManagementSystem.Api.Attributes;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreateEnvDto
    {
        [ValidGuid]
        public Guid Project { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
