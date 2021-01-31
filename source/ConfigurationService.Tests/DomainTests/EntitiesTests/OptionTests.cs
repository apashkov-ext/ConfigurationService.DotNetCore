using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Tests.TestSetup;
using ConfigurationService.Tests.TestSetup.Presets;
using Xunit;

namespace ConfigurationService.Tests.DomainTests.EntitiesTests
{
    public class OptionTests
    {
        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_NameIsCorrect(OptionGroup group)
        {
            var option = Option.Create(new OptionName(TestLiterals.Option.Name.Correct), 
                new Description(TestLiterals.Option.Description.Correct), 
                new OptionValue("Value"), group);
            Assert.Equal(TestLiterals.Option.Name.Correct, option.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_DescIsCorrect(OptionGroup group)
        {
            var option = Option.Create(new OptionName(TestLiterals.Option.Name.Correct),
                new Description(TestLiterals.Option.Description.Correct),
                new OptionValue("Value"), group);
            Assert.Equal(TestLiterals.Option.Description.Correct, option.Description.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void Create_CorrectData_ValueIsCorrect(OptionGroup group)
        {
            const string val = "Value";
            var option = Option.Create(new OptionName(TestLiterals.Option.Name.Correct),
                new Description(TestLiterals.Option.Description.Correct),
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

        //[Theory]
        //[ClassData(typeof(NonRootOptionGroup))]
        //public void UpdatValue_CorrectValue_ValueIsCorrect(OptionGroup group)
        //{
        //    const string val = "newVal";
        //    option.up(new Description(desc));
        //    Assert.Equal(desc, group.Description.Value);
        //}
    }
}
