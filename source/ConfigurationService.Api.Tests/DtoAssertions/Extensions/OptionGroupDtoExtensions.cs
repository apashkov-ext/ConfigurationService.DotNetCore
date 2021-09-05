using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Tests.DtoAssertions.Exceptions;
using ConfigurationService.Domain.Entities;
using System;
using System.Linq;

namespace ConfigurationService.Api.Tests.DtoAssertions.Extensions
{
    internal static class OptionGroupDtoExtensions
    {
        public static void IsEqualToModel(this OptionGroupDto dto, OptionGroup model)
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

            if (dto.Description != model.Description.Value)
            {
                throw UnexpectedPropertyValueException.Create(model.Description.Value, () => dto.Name);
            }

            var isRoot = model.Parent == null;
            if (isRoot != dto.Root)
            {
                throw UnexpectedPropertyValueException.Create(isRoot, () => dto.Root);
            }

            foreach (var option in model.Options)
            {
                var oId = option.Id.ToString();
                var o = dto.Options.FirstOrDefault(x => x.Id.Equals(oId, StringComparison.OrdinalIgnoreCase));
                if (o == null)
                {
                    throw NotFoundInValueException.Create(option, () => dto.Options);
                }

                o.IsEqualToModel(option);
            }

            foreach (var nested in model.NestedGroups)
            {
                var nId = nested.Id.ToString();
                var g = dto.NestedGroups.FirstOrDefault(x => x.Id.Equals(nId, StringComparison.OrdinalIgnoreCase));
                if (g == null)
                {
                    throw NotFoundInValueException.Create(nested, () => dto.NestedGroups);
                }

                g.IsEqualToModel(nested);
            }
        }
    }
}
