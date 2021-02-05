using System;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Tests;
using ConfigurationService.Tests.Fixtures;
using ConfigurationService.Tests.Presets;
using Xunit;

namespace ConfigurationService.Persistence.Tests
{
    public class OptionGroupsTests
    {
        [Fact]
        [ClassData(typeof(NonRootOptionGroup))]
        public async void Add_NotExistedGroup_Success(OptionGroup parent)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, parent)).Context;
            var g = await new OptionGroups(ctx).Add(parent.Id, TestLiterals.OptionGroup.Name.Correct, TestLiterals.OptionGroup.Description.Correct);
            Assert.Equal(g.Name.Value, TestLiterals.Project.Name.Correct);
        }

        [Theory]
        [ClassData(typeof(NonRootOptionGroupWithNested))]
        public async void Add_ExistedProject_Exception(OptionGroup parent, OptionGroup nested)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.OptionGroups, parent, nested)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new OptionGroups(ctx).Add(parent.Id, nested.Name.Value, TestLiterals.OptionGroup.Description.Correct));
        }
    }
}
