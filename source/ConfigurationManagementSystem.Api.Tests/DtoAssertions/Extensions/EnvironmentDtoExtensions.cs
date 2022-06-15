using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions;
using ConfigurationManagementSystem.Application.Dto;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions
{
    internal static class EnvironmentDtoExtensions
    {
        public static void IsEqualToModel(this ConfigurationDto dto, ConfigurationEntity model)
        {
            var id = model.Id.ToString();
            if (!dto.Id.Equals(id, System.StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(id, () => dto.Id);
            }

            if (dto.Name != model.Name.Value)
            {
                throw UnexpectedPropertyValueException.Create(model.Name.Value, () => dto.Name);
            }

            var projId = model.Application.Id.ToString();
            if (!dto.ApplicationId.Equals(projId, System.StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(projId, () => dto.ApplicationId);
            }
        }
    }
}
