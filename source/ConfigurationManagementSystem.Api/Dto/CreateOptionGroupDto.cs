using System;
using System.ComponentModel.DataAnnotations;
using ConfigurationManagementSystem.Api.Attributes;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreateOptionGroupDto
    {
        [ValidGuid]
        public Guid Parent { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
