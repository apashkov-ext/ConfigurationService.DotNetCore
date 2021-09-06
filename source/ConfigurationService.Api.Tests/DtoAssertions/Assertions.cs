using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Tests.DtoAssertions.Extensions;
using ConfigurationService.Domain.Entities;
using System.Collections.Generic;

namespace ConfigurationService.Api.Tests.DtoAssertions
{
    internal static class Assertions
    {
        public static void ProjectDtosAreEquivalentToModel(IEnumerable<ProjectDto> dtos, Project model)
        {
            foreach (var dto in dtos)
            {
                ProjectDtoIsEquivalentToModel(dto, model);
            }
        }

        public static void ProjectDtoIsEquivalentToModel(ProjectDto dto, Project model)
        {
            dto.IsEqualToModel(model);
        }

        public static void EnvironmentDtosAreEquivalentToModel(IEnumerable<EnvironmentDto> dtos, Environment model)
        {
            foreach (var dto in dtos)
            {
                EnvironmentDtoIsEquivalentToModel(dto, model);
            }
        }

        public static void EnvironmentDtoIsEquivalentToModel(EnvironmentDto dto, Environment model)
        {
            dto.IsEqualToModel(model);
        }
    }
}
