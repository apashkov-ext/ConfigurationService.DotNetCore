using ConfigurationManagementSystem.Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationManagementSystem.Application.Dto;

public class CreateOptionGroupDto
{
    [ValidGuid]
    public Guid Parent { get; set; }

    [Required]
    public string Name { get; set; }
}
