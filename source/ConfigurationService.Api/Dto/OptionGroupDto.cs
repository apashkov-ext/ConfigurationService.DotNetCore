using System.Collections.Generic;

namespace ConfigurationService.Api.Dto
{
    public class OptionGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<OptionDto> Options { get; set; }
        public IEnumerable<OptionGroupDto> NestedGroups { get; set; }
    }
}