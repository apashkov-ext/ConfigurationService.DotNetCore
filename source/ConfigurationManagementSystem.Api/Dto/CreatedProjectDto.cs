using System.Collections.Generic;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class CreatedProjectDto
    {
        public string ApiKey { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EnvironmentDto> Environments { get; set; }
    }
}
