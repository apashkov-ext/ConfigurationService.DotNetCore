using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain;
using System;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions
{
    internal static class OptionDtoExtensions
    {
        public static void IsEqualToModel(this OptionDto dto, Option model)
        {
            var id = model.Id.ToString();
            if (!dto.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(id, () => dto.Id);
            }

            if (dto.Name != model.Name.Value)
            {
                throw UnexpectedPropertyValueException.Create(model.Name.Value, () => dto.Name);
            }

            if (dto.Description != model.Description.Value)
            {
                throw UnexpectedPropertyValueException.Create(model.Description.Value, () => dto.Name);
            }

            var val = new JsonValueParser(dto.Value, dto.Type).Parse();
            var optionValue = TypeConversion.GetOptionValue(val, dto.Type);
            if (model.Value != optionValue)
            {
                throw UnexpectedPropertyValueException.Create(model.Value.Value, () => optionValue.Value);
            }
        }
    }
}
