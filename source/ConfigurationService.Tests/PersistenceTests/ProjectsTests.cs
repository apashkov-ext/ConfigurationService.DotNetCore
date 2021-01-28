using System;
using System.Linq;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Persistence;
using ConfigurationService.Tests.TestSetup;
using ConfigurationService.Tests.TestSetup.Fixtures;
using Xunit;

namespace ConfigurationService.Tests.PersistenceTests
{
    public class ProjectsTests
    {
        private const string CorrectProjectName = TestLiterals.Project.Name.Correct;
        private readonly Guid ApiKey = TestLiterals.Project.ApiKeys.Correct;
        private readonly DbContextFixture _contextFixture;

        public ProjectsTests()
        {
            _contextFixture = new DbContextFixture();
        }

        [Fact]
        public async void Add_NotExistedProject_Success()
        {
            var p = await new Projects(_contextFixture.EmptyContext).Add(CorrectProjectName);
            Assert.Equal(p.Name.Value, CorrectProjectName);
        }

        [Fact]
        public async void Add_ExistedProject_Exception()
        {
            await Assert.ThrowsAsync<AlreadyExistsException>(() => new Projects(_contextFixture.Context).Add(CorrectProjectName));
        }

        [Fact]
        public async void Remove_ExistedProject_Success()
        {
            var db = _contextFixture.Context;
            var id = db.Projects.First().Id;
            await new Projects(db).Remove(id);
        }

        [Fact]
        public async void Remove_NotExistedProject_Exception()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => new Projects(_contextFixture.EmptyContext).Remove(Guid.NewGuid()));
        }

        [Fact]
        public async void GetById_ExistedProject_Success()
        {
            var db = _contextFixture.Context;
            var first = db.Projects.First();
            var p = await new Projects(db).Get(first.Id);
            Assert.Equal(first, p);
        }

        [Fact]
        public async void GetById_NotExistedProject_Exception()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => new Projects(_contextFixture.EmptyContext).Get(Guid.NewGuid()));
        }

        [Theory]
        [InlineData("TestProject")]
        [InlineData("TestProj")]
        [InlineData("testproject")]
        public async void GetByName_ExistedProject_ReturnsCollectionWithTheProject(string name)
        {
            var result = await new Projects(_contextFixture.Context).Get(name);
            Assert.Contains(result, x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public async void GetByName_NotExistedProject_ReturnsEmptyCollection()
        {
            var result = await new Projects(_contextFixture.EmptyContext).Get(CorrectProjectName);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetByName_EmptyString_ReturnsAllProjects()
        {
            var result = await new Projects(_contextFixture.Context).Get("");
            Assert.NotEmpty(result);
        }
    }
}
