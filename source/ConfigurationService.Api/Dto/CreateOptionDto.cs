using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Dto
{
    public class CreateOptionDto
    {
        public Guid OptionGroup { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public OptionValueType Type { get; set; }

        [JsonPropertyName("value")]
        public object ValueKind { get; set; }

        [JsonIgnore]
        public object Value => new JsonValueParser(ValueKind, Type).Parse();
    }
}
