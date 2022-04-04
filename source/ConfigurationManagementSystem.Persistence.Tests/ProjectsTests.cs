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
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
            var p = await new Applications(ctx).AddAsync(expectedName);
            Assert.Equal(p.Name.Value, expectedName);
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void Add_ExistedProject_Exception(ApplicationEntity p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
            await Assert.ThrowsAsync<AlreadyExistsException>(() => new Applications(ctx).AddAsync(p.Name.Value));
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void Remove_ExistedProject_Success(ApplicationEntity p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
            await new Applications(ctx).RemoveAsync(p.Id);
        }

        [Fact]
        public async void Remove_NotExistedProject_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Applications(ctx).RemoveAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void GetById_ExistedProject_Success(ApplicationEntity p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
            var proj = await new Applications(ctx).GetAsync(p.Id);
            Assert.Equal(proj, p);
        }

        [Fact]
        public async void GetById_NotExistedProject_Exception()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
            await Assert.ThrowsAsync<NotFoundException>(() => new Applications(ctx).GetAsync(Guid.NewGuid()));
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void GetByName_ExistedProject_ReturnsCollectionWithTheProject(ApplicationEntity p)
        {
            const string search = "aPPlicaTion";
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
            var result = await new Applications(ctx).GetAsync(search, GetPaginationOptions());
            Assert.Contains(result.Data, x => x.Name.Value.StartsWith(search, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async void GetByName_NotExistedProject_ReturnsEmptyCollection()
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
            var result = await new Applications(ctx).GetAsync("TestProject", GetPaginationOptions());
            Assert.Empty(result.Data);
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void GetByName_EmptyString_ReturnsAllProjects(ApplicationEntity p)
        {
            var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
            var result = await new Applications(ctx).GetAsync("", GetPaginationOptions());
            Assert.NotEmpty(result.Data);
        }

        private static PaginationOptions GetPaginationOptions()
        {
            return new PaginationOptions(0, 100);
        }
    }
}
