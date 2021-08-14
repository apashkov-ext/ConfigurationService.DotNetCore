using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Dto
{
    public class UpdateOptionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public OptionValueType Type { get; set; }

        [JsonPropertyName("value")]
        public object ValueKind { get; set; }

        [JsonIgnore]
        public object Value => new JsonValueParser(ValueKind, Type).Parse();
    }
}
