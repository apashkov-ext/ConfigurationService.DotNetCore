using System;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Tests.Fixtures;
using ConfigurationService.Tests.Presets;
using Xunit;

namespace ConfigurationService.Persistence.Tests
{
    public class ProjectsTests
    {
        [Fact]
        public async void Add_NotExistedProject_Success()
        {
            const string expectedName = "TestProject";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            var p = await new Projects(ctx).Add(expectedName);
            Assert.Equal(p.Name.Value, expectedName);
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
            var result = await new Projects(ctx).Get("TestProject");
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
