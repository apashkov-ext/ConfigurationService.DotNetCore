using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Tests.Fixtures;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Persistence.Tests
{
    public class OptionGroupsTests
    {
        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public async void Add_NotExistedGroup_Success(OptionGroup parent)
        {
            const string name = "Validation";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, parent)).Context;
            var g = await new OptionGroups(ctx).Add(parent.Id, name, "Option group description");
            Assert.Equal(name, g.Name.Value);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithNested))]
        public async void Add_ExistedProject_Exception(OptionGroup parent, OptionGroup nested)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, parent, nested)).Context;
            await Assert.ThrowsAsync<InconsistentDataStateException>(() => new OptionGroups(ctx).Add(parent.Id, nested.Name.Value, "Option group description"));
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public async void UpdateNonRootGroup_EmptyName_Exception(OptionGroup g)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, g)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new OptionGroups(ctx).Update(g.Id, "", "desc"));
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroup))]
        public async void UpdateNonRootGroup_CorrectName_Success(OptionGroup g)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, g)).Context;
            await new OptionGroups(ctx).Update(g.Id, "OptGrName", "desc");
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithTwoNested))]
        public async void UpdateNonRootGroup_OtherGroupWithTheSameNameExists_Exception(OptionGroup parent, OptionGroup nested, OptionGroup nested2)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, parent, nested, nested2)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new OptionGroups(ctx).Update(nested2.Id, nested.Name.Value, "desc"));
        }

        [Theory]
        [ClassData(typeof(RootOptionGroup))]
        public async void UpdateRootGroup_CorrectName_Exception(OptionGroup root)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, root)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new OptionGroups(ctx).Update(root.Id, "OptGrName", "desc"));
        }
    }
}
