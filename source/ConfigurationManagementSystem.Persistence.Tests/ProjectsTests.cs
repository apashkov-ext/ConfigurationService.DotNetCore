using System;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Tests.Fixtures;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Persistence.Tests
{
    public class ProjectsTests
    {
        [Fact]
        public async void Add_NotExistedProject_Success()
        {
            const string expectedName = "TestProject";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            var p = await new Projects(ctx).AddAsync(expectedName);
            Assert.Equal(p.Name.Value, expectedName);
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Add_ExistedProject_Exception(Domain.Entities.Application p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            await Assert.ThrowsAsync<AlreadyExistsException>(() => new Projects(ctx).AddAsync(p.Name.Value));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void Remove_ExistedProject_Success(Domain.Entities.Application p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            await new Projects(ctx).RemoveAsync(p.Id);
        }

        [Fact]
        public async void Remove_NotExistedProject_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Projects(ctx).RemoveAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void GetById_ExistedProject_Success(Domain.Entities.Application p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            var proj = await new Projects(ctx).GetAsync(p.Id);
            Assert.Equal(proj, p);
        }

        [Fact]
        public async void GetById_NotExistedProject_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Projects(ctx).GetAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void GetByName_ExistedProject_ReturnsCollectionWithTheProject(Domain.Entities.Application p)
        {
            const string search = "pRojeCt";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            var result = await new Projects(ctx).GetAsync(search, GetPaginationOptions());
            Assert.Contains(result.Data, x => x.Name.Value.StartsWith(search, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async void GetByName_NotExistedProject_ReturnsEmptyCollection()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects)).Context;
            var result = await new Projects(ctx).GetAsync("TestProject", GetPaginationOptions());
            Assert.Empty(result.Data);
        }

        [Theory]
        [ClassData(typeof(EmptyProject))]
        public async void GetByName_EmptyString_ReturnsAllProjects(Domain.Entities.Application p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Projects, p)).Context;
            var result = await new Projects(ctx).GetAsync("", GetPaginationOptions());
            Assert.NotEmpty(result.Data);
        }

        private static PaginationOptions GetPaginationOptions()
        {
            return new PaginationOptions(0, 100);
        }
    }
}
