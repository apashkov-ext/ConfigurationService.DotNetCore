using System;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Tests.Fixtures;
using ConfigurationService.Tests.Presets;
using Xunit;

namespace ConfigurationService.Persistence.Tests
{
    public class OptionsTests
    {
        [Theory]
        [ClassData(typeof(TwoOptionsOfSameOptionGroup))]
        public async void UpdateNonRootGroup_OtherGroupWithTheSameNameExists_Exception(Option op1, Option op2)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Options, op1, op2)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new Options(ctx).UpdateAsync(op1.Id, op2.Name.Value, "desc", op1.Value.Value));
        }
    }
}
