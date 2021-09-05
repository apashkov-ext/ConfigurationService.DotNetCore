using Environment = ConfigurationService.Domain.Entities.Environment;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using ConfigurationService.Api.Tests.DtoAssertions.Exceptions;
using System.Linq.Expressions;

namespace ConfigurationService.Api.Tests.DtoAssertions.Extensions
{
    internal static class ProjectDtoExtensions
    {
        public static void IsEqualToModel(this ProjectDto dto, Project model)
        {
            var id = model.Id.ToString();
            if (!dto.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(id, () => dto.Id);
            }

            if (dto.Name != model.Name.Value)
            {
                throw UnexpectedPropertyValueException.Create(model.Name.Value, () => dto.Name);
            }

            EnvironmentsAreEqual(model.Environments, dto.Environments, () => dto.Environments);
        }

        public static void IsEqualToModel(this CreatedProjectDto dto, Project model)
        {
            var id = model.Id.ToString();
            if (!dto.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(id, () => dto.Id);
            }

            if (model.Name.Value != dto.Name)
            {
                throw UnexpectedPropertyValueException.Create(model.Name.Value, () => dto.Name);
            }

            var key = model.ApiKey.Value.ToString();
            if (!dto.ApiKey.Equals(key, StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(key, () => dto.ApiKey);
            }

            EnvironmentsAreEqual(model.Environments, dto.Environments, () => dto.Environments);
        }

        private static void EnvironmentsAreEqual(IEnumerable<Environment> envs, IEnumerable<EnvironmentDto> dtos, Expression<Func<object>> actualPropertySelector)
        {
            foreach (var e in envs)
            {
                var id = e.Id.ToString();
                var env = dtos.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (env == null)
                {
                    throw NotFoundInValueException.Create(e, actualPropertySelector);
                }

                env.IsEqualToModel(e);
            }
        }
    }
}
