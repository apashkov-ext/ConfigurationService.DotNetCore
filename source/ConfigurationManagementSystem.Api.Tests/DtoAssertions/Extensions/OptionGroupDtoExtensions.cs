using System;
using System.Linq;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions.Exceptions;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Tests.DtoAssertions.Extensions
{
    internal static class OptionGroupDtoExtensions
    {
        public static void IsEqualToModel(this OptionGroupDto dto, OptionGroupEntity model)
        {
            var id = model.Id.ToString();
            if (!dto.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(id, () => dto.Id);
            }

            if (dto.ParentId != null && model.Parent != null)
            {
                if (!dto.ParentId.Equals(model.Parent.Id.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    throw UnexpectedPropertyValueException.Create(model.Parent.Id, () => dto.ParentId);
                }
            } 
            else if (dto.ParentId == null || model.Parent == null)
            {
                throw UnexpectedPropertyValueException.Create(model.Parent?.Id, () => dto.ParentId);
            }

            var configId = model.Configuration.Id.ToString();
            if (!dto.ConfigurationId.Equals(configId, StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(configId, () => dto.ConfigurationId);
            }

            if (!dto.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                throw UnexpectedPropertyValueException.Create(id, () => dto.Id);
            }

            if (dto.Name != model.Name.Value)
            {
                throw UnexpectedPropertyValueException.Create(model.Name.Value, () => dto.Name);
            }

            var isRoot = model.Parent == null;
            if (isRoot != dto.Root)
            {
                throw UnexpectedPropertyValueException.Create(isRoot, () => dto.Root);
            }


        }
    }
}
