using System;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.Exceptions;
using ConfigurationManagementSystem.Tests.Fixtures;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;
using ConfigurationEntity = ConfigurationManagementSystem.Domain.Entities.ConfigurationEntity;

namespace ConfigurationManagementSystem.Persistence.Tests
{
    public class EnvironmentsTests
    {
        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void Add_NotExistedEnv_Success(Domain.Entities.ApplicationEntity p)
        {
            const string envName = "Dev";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p).WithSet(s => s.Configurations)).Context;
            var env = await new Environments(ctx).AddAsync(p.Id, envName);
            Assert.Equal(envName, env.Name.Value);
        }

        [Theory]
        [ClassData(typeof(ApplicationWithConfiguration))]
        public async void Add_ExistedEnv_Exception(Domain.Entities.ApplicationEntity p, ConfigurationEntity e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p).WithSet(s => s.Configurations, e)).Context;
            await Assert.ThrowsAsync<InconsistentDataStateException>(() => new Environments(ctx).AddAsync(p.Id, e.Name.Value));
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void Add_IncorrectName_Exception(Domain.Entities.ApplicationEntity p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p).WithSet(s => s.Configurations)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new Environments(ctx).AddAsync(p.Id, "123"));
        }

        [Theory]
        [ClassData(typeof(ApplicationWithConfiguration))]
        public async void Remove_ExistedEnv_Success(Domain.Entities.ApplicationEntity p, ConfigurationEntity e)
        {
            var ctx = new DbContextFixture(x => 
                x.WithSet(s => s.Applications, p)
                .WithSet(s => s.Configurations, e)
                .WithSet(x => x.OptionGroups)
                .WithSet(s => s.Options))
                .Context;
            await new Environments(ctx).RemoveAsync(e.Id);
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void Remove_NotExistedEnv_Exception(Domain.Entities.ApplicationEntity p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p).WithSet(s => s.Configurations)).Context;
            await Assert.ThrowsAsync<EntityNotFoundException>(() => new Environments(ctx).RemoveAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void GetById_ExistedEnv_ReturnsEnv(ConfigurationEntity e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Configurations, e)).Context;
            var env = await new Environments(ctx).GetAsync(e.Id);
            Assert.Equal(e.Id, env.Id);
        }

        [Fact]
        public async void GetById_NotExistedEnv_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Configurations)).Context;
            await Assert.ThrowsAsync<EntityNotFoundException>(() => new Environments(ctx).GetAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void Update_NotExistedEnv_Exception(ConfigurationEntity e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Configurations)).Context;
            await Assert.ThrowsAsync<EntityNotFoundException>(() => new Environments(ctx).UpdateAsync(e.Id, "NewEnv"));
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void Update_ExistedEnvCorrectName_Success(ConfigurationEntity e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Configurations, e)).Context;
            await new Environments(ctx).UpdateAsync(e.Id, "NewEnv");
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void Update_AlreadyExistedName_Exception(ConfigurationEntity e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Configurations, e)).Context;
            await Assert.ThrowsAsync<InconsistentDataStateException>(() => new Environments(ctx).UpdateAsync(e.Id, e.Name.Value));
        }
    }
}
