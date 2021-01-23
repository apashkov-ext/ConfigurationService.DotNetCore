using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Dto
{
    public class UpdateOptionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public OptionValueType? Type { get; set; }
    }
}
