using System.Text.Json.Serialization;
using ConfigurationService.Api.Converters;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Dto
{
    public class UpdateOptionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(ObjectToInferredTypesConverter))]
        public object Value { get; set; }
        public OptionValueType? Type { get; set; }
    }
}
