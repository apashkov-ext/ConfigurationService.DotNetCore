using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Domain.Tests.EntitiesTests
{
    public class OptionGroupTests
    {
        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public void Create_CorrectData_ParentIsCorrect(ConfigurationEntity e)
        {
            var group = OptionGroup.Create(
                new OptionGroupName("Validation"), 
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
        public void AddNested_NotExisted_NameEqualsWithNewName(OptionGroup group)
        {
            const string name = "Validation" + "Nested"; 
            group.AddNestedGroup(new OptionGroupName(name));
            Assert.Contains(group.NestedGroups, x => x.Name.Value == name);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddNested_NotExisted_ParentIsCorrect(OptionGroup group)
        {
            var nested = group.AddNestedGroup(new OptionGroupName("Validation" + "Nested"));
            Assert.Equal(group, nested.Parent);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithNested))]
        public void AddNested_Existed_Exception(OptionGroup parent, OptionGroup nested)
        {
            Assert.Throws<InconsistentDataStateException>(() => parent.AddNestedGroup(new OptionGroupName(nested.Name.Value)));
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddNested_EmptyStringName_Exception(OptionGroup parent)
        {
            Assert.Throws<InconsistentDataStateException>(() => parent.AddNestedGroup(new OptionGroupName("")));
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddOption_NotExisted_ValueIsCorrect(OptionGroup group)
        {
            const string val = "Value";
            var option = group.AddOption(
                new OptionName("Enabled"),
                new OptionValue(val));
            Assert.Equal(val, option.Value.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public void AddOption_NotExisted_NameIsCorrect(OptionGroup group)
        {
            var option = group.AddOption(
                new OptionName("Enabled"),
                new OptionValue("Value"));
            Assert.Equal("Enabled", option.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithOption))]
        public void AddOption_Existed_Exception(OptionGroup group, OptionEntity option)
        {
            const string val = "Value";
            Assert.Throws<InconsistentDataStateException>(() => group.AddOption(new OptionName(option.Name.Value), 
                new OptionValue(val)));
        }
    }
}
