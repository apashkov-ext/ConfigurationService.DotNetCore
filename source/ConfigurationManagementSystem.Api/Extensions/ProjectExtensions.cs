using System.Linq;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class ProjectExtensions
    {
        public static ProjectDto ToDto(this Domain.Entities.Application source)
        {
            return new ProjectDto
            {
                Id = source.Id.ToString(),
                Name = source.Name.Value,
                Environments = source.Environments.Select(x => x.ToDto())
            };
        }

        public static CreatedProjectDto ToCreatedProjectDto(this Domain.Entities.Application source)
        {
            return new CreatedProjectDto
            {
                Id = source.Id.ToString(),
                Name = source.Name.Value,
                Environments = source.Environments.Select(x => x.ToDto()),
                ApiKey = source.ApiKey.Value.ToString()
            };
        }
    }
}
