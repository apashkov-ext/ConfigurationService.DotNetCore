using System.Collections.Generic;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreatedApplicationDto
    {
        public string ApiKey { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ConfigurationDto> Configurations { get; set; }
    }
}
