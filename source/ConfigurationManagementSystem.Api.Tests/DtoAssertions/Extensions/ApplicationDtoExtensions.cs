using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions
{
    internal static class ApplicationDtoExtensions
    {
        public static void IsEqualToModel(this ApplicationDto dto, ApplicationEntity model)
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

            EnvironmentsAreEqual(model.Configurations, dto.Configurations, () => dto.Configurations);
        }

        public static void IsEqualToModel(this CreatedApplicationDto dto, ApplicationEntity model)
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

            EnvironmentsAreEqual(model.Configurations, dto.Configurations, () => dto.Configurations);
        }

        private static void EnvironmentsAreEqual(IEnumerable<ConfigurationEntity> envs, IEnumerable<ConfigurationDto> dtos, Expression<Func<object>> actualPropertySelector)
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
