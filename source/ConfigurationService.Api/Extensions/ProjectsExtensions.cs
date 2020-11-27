using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class ProjectsExtensions
    {
        public static IEnumerable<ProjectDto> ToDtoItems(this IEnumerable<Project> source)
        {
            return source.Select(ToDto);
        }

        public static ProjectDto ToDto(this Project source)
        {
            return new ProjectDto
            {
                Name = source.Name.Value,
                Configurations = source.Configurations.Select(x => x.ToDto())
            };
        }
    }
}
