using System.Text.Json.Serialization;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class UpdateOptionDto
    {
        public string Name { get; set; }
        public OptionValueType Type { get; set; }

        [JsonPropertyName("value")]
        public object ValueKind { get; set; }

        [JsonIgnore]
        public object Value => new JsonValueParser(ValueKind, Type).Parse();
    }
}
