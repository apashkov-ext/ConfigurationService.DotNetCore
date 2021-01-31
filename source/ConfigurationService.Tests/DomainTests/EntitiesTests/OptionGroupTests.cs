using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Tests.TestSetup;
using ConfigurationService.Tests.TestSetup.Presets;
using Xunit;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Tests.DomainTests.EntitiesTests
{
    public class OptionGroupTests
    {
        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public void Create_CorrectData_ParentIsCorrect(Environment e)
        {
            var group = OptionGroup.Create(
                new OptionGroupName(TestLiterals.OptionGroup.Name.Correct), 
                new Description(TestLiterals.OptionGroup.Description.Correct), 
                e, 
                e.GetRootOptionGroop());
            Assert.Equal(e.GetRootOptionGroop(), group.Parent);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void UpdateName_CorrectName_Success(OptionGroup group)
        {
            const string name = "NewOptionGroupName";
            group.UpdateName(new OptionGroupName(name));
            Assert.Equal(name, group.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void UpdateDesc_CorrectDesc_Success(OptionGroup group)
        {
            const string desc = "New description";
            group.UpdateDescription(new Description(desc));
            Assert.Equal(desc, group.Description.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddNested_NotExisted_NameEqualsWithNewName(OptionGroup group)
        {
            const string name = TestLiterals.OptionGroup.Name.Correct + "Nested"; 
            group.AddNestedGroup(new OptionGroupName(name), new Description("New Nested Group"));
            Assert.Contains(group.NestedGroups, x => x.Name.Value == name);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddNested_NotExisted_ParrentIsCorrect(OptionGroup group)
        {
            var nested = group.AddNestedGroup(new OptionGroupName(TestLiterals.OptionGroup.Name.Correct + "Nested"), new Description("New Nested Group"));
            Assert.Equal(group, nested.Parent);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithNested))]
        public void AddNested_Existed_Exception(OptionGroup parent, OptionGroup nested)
        {
            Assert.Throws<ApplicationException>(() => parent.AddNestedGroup(new OptionGroupName(nested.Name.Value), new Description("New Nested Group")));
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddOption_NotExisted_ValueIsCorrect(OptionGroup group)
        {
            const string val = "Value";
            var option = group.AddOption(
                new OptionName(TestLiterals.Option.Name.Correct),
                new Description(TestLiterals.Option.Description.Correct),
                new OptionValue(val));
            Assert.Equal(val, option.Value.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddOption_NotExisted_NameIsCorrect(OptionGroup group)
        {
            var option = group.AddOption(
                new OptionName(TestLiterals.Option.Name.Correct),
                new Description(TestLiterals.Option.Description.Correct),
                new OptionValue("Value"));
            Assert.Equal(TestLiterals.Option.Name.Correct, option.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithOption))]
        public void AddOption_Existed_Exception(OptionGroup group, Option option)
        {
            const string val = "Value";
            Assert.Throws<ApplicationException>(() => group.AddOption(new OptionName(option.Name.Value), 
                new Description(TestLiterals.Option.Description.Correct),
                new OptionValue(val)));
        }
    }
}
