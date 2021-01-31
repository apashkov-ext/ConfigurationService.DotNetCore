using System;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Persistence;
using ConfigurationService.Tests.TestSetup;
using ConfigurationService.Tests.TestSetup.Fixtures;
using ConfigurationService.Tests.TestSetup.Presets;
using Xunit;

namespace ConfigurationService.Tests.PersistenceTests
{
    public class ProjectsTests
    {
        [Fact]
        public async void Add_NotExistedProject_Success()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            var p = await new Projects(ctx).Add(TestLiterals.Project.Name.Correct);
            Assert.Equal(p.Name.Value, TestLiterals.Project.Name.Correct);
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Add_ExistedProject_Exception(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            await Assert.ThrowsAsync<AlreadyExistsException>(() => new Projects(ctx).Add(p.Name.Value));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Remove_ExistedProject_Success(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            await new Projects(ctx).Remove(p.Id);
        }

        [Fact]
        public async void Remove_NotExistedProject_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Projects(ctx).Remove(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void GetById_ExistedProject_Success(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            var proj = await new Projects(ctx).Get(p.Id);
            Assert.Equal(proj, p);
        }

        [Fact]
        public async void GetById_NotExistedProject_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Projects(ctx).Get(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void GetByName_ExistedProject_ReturnsCollectionWithTheProject(Project p)
        {
            const string search = "pRojeCt";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            var result = await new Projects(ctx).Get(search);
            Assert.Contains(result, x => x.Name.Value.StartsWith(search, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async void GetByName_NotExistedProject_ReturnsEmptyCollection()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            var result = await new Projects(ctx).Get(TestLiterals.Project.Name.Correct);
            Assert.Empty(result);
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void GetByName_EmptyString_ReturnsAllProjects(Project p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            var result = await new Projects(ctx).Get("");
            Assert.NotEmpty(result);
        }
    }
}
