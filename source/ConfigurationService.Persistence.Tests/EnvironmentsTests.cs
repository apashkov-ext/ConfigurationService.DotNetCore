using System;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Tests;
using ConfigurationService.Tests.Fixtures;
using ConfigurationService.Tests.Presets;
using Xunit;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence.Tests
{
    public class EnvironmentsTests
    {
        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Add_NotExistedEnv_Success(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p).WithSet(s => s.Environments)).Context;
            var env = await new Environments(ctx).AddAsync(p.Id, TestLiterals.Environment.Name.Correct);
            Assert.Equal(env.Name.Value, TestLiterals.Environment.Name.Correct);
        }

        [Theory]
        [ClassData(typeof(ProjectWithEnvironment))]
        public async void Add_ExistedEnv_Exception(Project p, Environment e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p).WithSet(s => s.Environments, e)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new Environments(ctx).AddAsync(p.Id, e.Name.Value));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Add_IncorrectName_Exception(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p).WithSet(s => s.Environments)).Context;
            await Assert.ThrowsAsync<ApplicationException>(() => new Environments(ctx).AddAsync(p.Id, "123"));
        }

        [Theory]
        [ClassData(typeof(ProjectWithEnvironment))]
        public async void Remove_ExistedEnv_Success(Project p, Environment e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p).WithSet(s => s.Environments, e)).Context;
            await new Environments(ctx).RemoveAsync(e.Id);
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Remove_NotExistedEnv_Exception(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p).WithSet(s => s.Environments)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Environments(ctx).RemoveAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void GetById_ExistedEnv_ReturnsEnv(Environment e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Environments, e)).Context;
            var env = await new Environments(ctx).GetAsync(e.Id);
            Assert.Equal(e.Id, env.Id);
        }

        [Fact]
        public async void GetById_NotExistedEnv_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Environments)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Environments(ctx).GetAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void Update_NotExistedEnv_Exception(Environment e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Environments)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Environments(ctx).UpdateAsync(e.Id, "NewEnv"));
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void Update_ExistedEnvCorrectName_Success(Environment e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Environments, e)).Context;
            await new Environments(ctx).UpdateAsync(e.Id, "NewEnv");
        }

        [Theory]
        [ClassData(typeof(ValidEnvironment))]
        public async void Update_AlreadyExistedName_Exception(Environment e)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Environments, e)).Context;
            await Assert.ThrowsAsync<InconsistentDataState>(() => new Environments(ctx).UpdateAsync(e.Id, e.Name.Value));
        }
    }
}
