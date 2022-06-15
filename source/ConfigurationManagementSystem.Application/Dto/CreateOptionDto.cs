using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ConfigurationManagementSystem.Application.Attributes;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application.Dto;

public class CreateOptionDto
{
    [ValidGuid]
    public Guid OptionGroup { get; set; }

    [Required]
    public string Name { get; set; }
    public OptionValueType Type { get; set; }

    [JsonPropertyName("value")]
    public object ValueKind { get; set; }

    [JsonIgnore]
    public object Value => new JsonValueParser(ValueKind, Type).Parse();
}
