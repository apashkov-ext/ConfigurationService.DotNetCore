using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
{
    public class OptionTests
    {
        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_NameIsCorrect(OptionGroup group)
        {
            var option = OptionEntity.Create(new OptionName("Enabled"), 
                new OptionValue("Value"), group);
            Assert.Equal("Enabled", option.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_ValueIsCorrect(OptionGroup group)
        {
            const string val = "Value";
            var option = OptionEntity.Create(new OptionName("Enabled"),
                new OptionValue(val), group);
            Assert.Equal(val, option.Value.Value);
        }

        [Theory]
        [ClassData(typeof(OptionWithRelatedEntities))]
        public void UpdateName_CorrectName_NameIsCorrect(OptionEntity option)
        {
            const string name = "NewOptionName";
            option.UpdateName(new OptionName(name));
            Assert.Equal(name, option.Name.Value);
        }
    }
}
