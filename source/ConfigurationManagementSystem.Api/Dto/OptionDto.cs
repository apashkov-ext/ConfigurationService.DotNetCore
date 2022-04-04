using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class OptionDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public OptionValueType Type { get; set; }
    }
}