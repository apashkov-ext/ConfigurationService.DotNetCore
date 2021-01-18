using System.Linq;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Api.Extensions
{
    internal static class ProjectExtensions
    {
        public static ProjectDto ToDto(this Project source)
        {
            return new ProjectDto
            {
                Name = source.Name.Value,
                Configurations = source.Environments.Select(x => x.ToDto())
            };
        }

        public static CreatedProjectDto ToCreatedProjectDto(this Project source)
        {
            return new CreatedProjectDto
            {
                Name = source.Name.Value,
                Configurations = source.Environments.Select(x => x.ToDto()),
                ApiKey = source.ApiKey.Value.ToString()
            };
        }
    }
}
