using ConfigurationManagementSystem.Api.Dto;
using Environment = ConfigurationManagementSystem.Domain.Entities.Environment;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static EnvironmentDto ToDto(this Environment source)
        {
            var root = source.GetRootOptionGroop();
            return new EnvironmentDto
            {
                Id = source.Id.ToString(),
                ProjectId = source.Project.Id.ToString(),
                Name = source.Name.Value,
                OptionGroup = root.ToDto()
            };
        }
    }
}
