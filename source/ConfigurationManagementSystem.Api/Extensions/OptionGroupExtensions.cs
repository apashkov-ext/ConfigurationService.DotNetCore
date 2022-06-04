using System.Linq;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class OptionGroupExtensions
    {
        public static OptionGroupDto ToDto(this OptionGroupEntity source)
        {
            var options = source.Options.Select(x => x.ToDto());
            return new OptionGroupDto
            {
                Id = source.Id.ToString(),
                ParentId = source.Parent?.Id.ToString(),
                ConfigurationId = source.Configuration.Id.ToString(),
                Name = source.Name.Value,
                Root = source.Parent == null
            };
        }
    }
}
