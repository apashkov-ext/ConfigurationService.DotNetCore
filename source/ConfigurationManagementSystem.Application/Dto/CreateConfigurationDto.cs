using ConfigurationManagementSystem.Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConfigurationManagementSystem.Application.Dto;

public class CreateConfigurationDto
{
    [ValidGuid]
    public Guid Application { get; set; }

    [Required]
    public string Name { get; set; }
}
