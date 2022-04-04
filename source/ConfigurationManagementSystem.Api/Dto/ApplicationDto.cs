using System.Collections.Generic;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class ApplicationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ConfigurationDto> Configurations { get; set; }
    }
}
