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
            var option = Option.Create(new OptionName("Enabled"), 
                new Description("Option description"), 
                new OptionValue("Value"), group);
            Assert.Equal("Enabled", option.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_DescIsCorrect(OptionGroup group)
        {
            var option = Option.Create(new OptionName("Enabled"),
                new Description("Option description"),
                new OptionValue("Value"), group);
            Assert.Equal("Option description", option.Description.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_ValueIsCorrect(OptionGroup group)
        {
            const string val = "Value";
            var option = Option.Create(new OptionName("Enabled"),
                new Description("Option description"),
                new OptionValue(val), group);
            Assert.Equal(val, option.Value.Value);
        }

        [Theory]
        [ClassData(typeof(OptionWithRelatedEntities))]
        public void UpdateName_CorrectName_NameIsCorrect(Option option)
        {
            const string name = "NewOptionName";
            option.UpdateName(new OptionName(name));
            Assert.Equal(name, option.Name.Value);
        }

        [Theory]
        [ClassData(typeof(OptionWithRelatedEntities))]
        public void UpdateDesc_CorrectDesc_DescIsCorrect(Option option)
        {
            const string desc = "New option description";
            option.UpdateDescription(new Description(desc));
            Assert.Equal(desc, option.Description.Value);
        }
    }
}
