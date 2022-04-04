using System;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Tests.Fixtures;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Persistence.Tests
{
    public class OptionsTests
    {
        [Theory]
        [ClassData(typeof(TwoOptionsOfSameOptionGroup))]
        public async void UpdateNonRootGroup_OtherGroupWithTheSameNameExists_Exception(Option op1, Option op2)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Options, op1, op2)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new Options(ctx).UpdateAsync(op1.Id, op2.Name.Value, op1.Value.Value));
        }
    }
}
