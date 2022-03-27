using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions;
using System.Collections.Generic;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions
{
    internal static class Assertions
    {
        public static void ProjectDtosAreEquivalentToModel(IEnumerable<ProjectDto> dtos, Domain.Entities.Application model)
        {
            foreach (var dto in dtos)
            {
                ProjectDtoIsEquivalentToModel(dto, model);
            }
        }

        public static void ProjectDtoIsEquivalentToModel(ProjectDto dto, Domain.Entities.Application model)
        {
            dto.IsEqualToModel(model);
        }

        public static void EnvironmentDtosAreEquivalentToModel(IEnumerable<EnvironmentDto> dtos, Configuration model)
        {
            foreach (var dto in dtos)
            {
                EnvironmentDtoIsEquivalentToModel(dto, model);
            }
        }

        public static void EnvironmentDtoIsEquivalentToModel(EnvironmentDto dto, Configuration model)
        {
            dto.IsEqualToModel(model);
        }
    }
}
