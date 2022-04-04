using System.Linq;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class ApplicationExtensions
    {
        public static ApplicationDto ToDto(this ApplicationEntity source)
        {
            return new ApplicationDto
            {
                Id = source.Id.ToString(),
                Name = source.Name.Value,
                Configurations = source.Configurations.Select(x => x.ToDto())
            };
        }

        public static CreatedApplicationDto ToCreatedApplicationDto(this ApplicationEntity source)
        {
            return new CreatedApplicationDto
            {
                Id = source.Id.ToString(),
                Name = source.Name.Value,
                Configurations = source.Configurations.Select(x => x.ToDto()),
                ApiKey = source.ApiKey.Value.ToString()
            };
        }
    }
}
