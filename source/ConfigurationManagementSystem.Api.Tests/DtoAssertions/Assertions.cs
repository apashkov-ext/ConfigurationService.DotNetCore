using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions;
using System.Collections.Generic;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Application.Dto;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions
{
    internal static class Assertions
    {
        public static void ApplicationDtosAreEquivalentToModel(IEnumerable<ApplicationDto> dtos, Domain.Entities.ApplicationEntity model)
        {
            foreach (var dto in dtos)
            {
                ApplicationDtoIsEquivalentToModel(dto, model);
            }
        }

        public static void ApplicationDtoIsEquivalentToModel(ApplicationDto dto, Domain.Entities.ApplicationEntity model)
        {
            dto.IsEqualToModel(model);
        }

        public static void ConfigurationDtosAreEquivalentToModel(IEnumerable<ConfigurationDto> dtos, ConfigurationEntity model)
        {
            foreach (var dto in dtos)
            {
                ConfigurationDtoIsEquivalentToModel(dto, model);
            }
        }

        public static void ConfigurationDtoIsEquivalentToModel(ConfigurationDto dto, ConfigurationEntity model)
        {
            dto.IsEqualToModel(model);
        }
    }
}
