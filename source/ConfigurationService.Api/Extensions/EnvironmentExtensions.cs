using ConfigurationService.Api.Dto;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Api.Extensions
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
