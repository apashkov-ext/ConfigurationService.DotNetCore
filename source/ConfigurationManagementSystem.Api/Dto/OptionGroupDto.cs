using System.Collections.Generic;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class OptionGroupDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<OptionDto> Options { get; set; }
        public IEnumerable<OptionGroupDto> NestedGroups { get; set; }
        public bool Root { get; set; }
    }
}