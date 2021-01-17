using System.Collections.Generic;

namespace ConfigurationService.Api.Dto
{
    public class EnvironmentDto
    {
        public string Name { get; set; }
        public OptionGroupDto OptionGroup { get; set; }
    }
}