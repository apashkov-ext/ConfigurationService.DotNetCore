using ConfigurationManagementSystem.Api.Dto;
using Configuration = ConfigurationManagementSystem.Domain.Entities.Configuration;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions
{
    internal static class EnvironmentDtoExtensions
    {
        public static void IsEqualToModel(this EnvironmentDto dto, Configuration model)
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

            var projId = model.Project.Id.ToString();
            if (!dto.ProjectId.Equals(projId, System.StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(projId, () => dto.ProjectId);
            }

            var root = model.GetRootOptionGroop();
            if (dto.OptionGroup == null)
            {
                throw UnexpectedPropertyValueException.Create(root, () => dto.OptionGroup);
            }

            dto.OptionGroup.IsEqualToModel(root);
        }
    }
}
