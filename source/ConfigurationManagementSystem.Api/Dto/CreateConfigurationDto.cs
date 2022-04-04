using System;
using System.ComponentModel.DataAnnotations;
using ConfigurationManagementSystem.Api.Attributes;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreateConfigurationDto
    {
        [ValidGuid]
        public Guid Application { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
