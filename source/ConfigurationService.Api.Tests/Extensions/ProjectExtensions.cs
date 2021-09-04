using Environment = ConfigurationService.Domain.Entities.Environment;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationService.Api.Tests.Extensions
{
    internal static class ProjectExtensions
    {
        public static bool IsEqualToDto(this Project project, ProjectDto dto)
        {
            if (project.Id.ToString() != dto.Id)
            {
                return false;
            }

            if (project.Name.Value != dto.Name)
            {
                return false;
            }

            if (!EnvironmentsAreEqual(project.Environments, dto.Environments))
            {
                return false;
            }

            return true;
        }

        public static bool IsEqualToDtoWithApiKey(this Project project, CreatedProjectDto dto)
        {
            if (project.Id.ToString() != dto.Id)
            {
                return false;
            }

            if (project.Name.Value != dto.Name)
            {
                return false;
            }

            if (project.ApiKey.Value.ToString() != dto.ApiKey)
            {
                return false;
            }

            if (!EnvironmentsAreEqual(project.Environments, dto.Environments))
            {
                return false;
            }

            return true;
        }

        private static bool EnvironmentsAreEqual(IEnumerable<Environment> envs, IEnumerable<EnvironmentDto> dtos)
        {
            foreach (var e in envs)
            {
                var id = e.Id.ToString();
                var env = dtos.FirstOrDefault(x => x.Id == id);
                if (env == null || !e.IsEqualToDto(env))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
