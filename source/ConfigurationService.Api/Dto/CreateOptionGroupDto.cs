using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationService.Api.Dto
{
    public class CreateOptionGroupDto
    {
        public Guid Parent { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
