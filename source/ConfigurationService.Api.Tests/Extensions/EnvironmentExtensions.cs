using Environment = ConfigurationService.Domain.Entities.Environment;
using ConfigurationService.Api.Dto;

namespace ConfigurationService.Api.Tests.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static bool IsEqualToDto(this Environment env, EnvironmentDto dto)
        {
            if (env.Id.ToString() != dto.Id)
            {
                return false;
            }

            if (env.Name.Value != dto.Name)
            {
                return false;
            }

            if (env.Project.Id.ToString() != dto.ProjectId)
            {
                return false;
            }

            var root = env.GetRootOptionGroop();
            if (dto.OptionGroup == null || !root.IsEqualToDto(dto.OptionGroup))
            {
                return false;
            }

            return true;
        }
    }
}
