using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Persistence;
using ConfigurationService.Tests.TestSetup.Fixtures;
using Xunit;

namespace ConfigurationService.Tests.PersistenceTests
{
    public class EnvironmentsTests
    {
        private readonly DbContextFixture _contextFixture;

        public EnvironmentsTests()
        {
            _contextFixture = new DbContextFixture();
        }

        //[Fact]
        //public async void Add_NotExistedEnv_Success()
        //{

        //    const string name = "Dev";
        //    var p = await new Environments(_contextFixture.EmptyContext).Add();
        //    Assert.Equal(p.Name.Value, name);
        //}

        //[Fact]
        //public async void Add_ExistedProject_Exception()
        //{
        //    await Assert.ThrowsAsync<AlreadyExistsException>(() => new Projects(_contextFixture.Context).Add("TestProject"));
        //}

        //[Fact]
        //public async void Remove_ExistedProject_Success()
        //{
        //    var db = _contextFixture.Context;
        //    var id = db.Projects.First().Id;
        //    await new Projects(db).Remove(id);
        //}

        //[Fact]
        //public async void Remove_NotExistedProject_Exception()
        //{
        //    await Assert.ThrowsAsync<NotFoundException>(() => new Projects(_contextFixture.EmptyContext).Remove(Guid.NewGuid()));
        //}

        //[Fact]
        //public async void GetById_ExistedProject_Success()
        //{
        //    var db = _contextFixture.Context;
        //    var id = db.Projects.First().Id;
        //    var p = await new Projects(db).Get(id);
        //    Assert.Equal(id, p.Id);
        //}

        //[Fact]
        //public async void GetById_NotExistedProject_Exception()
        //{
        //    await Assert.ThrowsAsync<NotFoundException>(() => new Projects(_contextFixture.EmptyContext).Get(Guid.NewGuid()));
        //}

        //[Theory]
        //[InlineData("TestProject")]
        //[InlineData("TestProj")]
        //[InlineData("testproject")]
        //public async void GetByName_ExistedProject_ReturnsCollectionWithTheProject(string name)
        //{
        //    var result = await new Projects(_contextFixture.Context).Get(name);
        //    Assert.Contains(result, x => x.Name.Value.StartsWith(name, StringComparison.InvariantCultureIgnoreCase));
        //}

        //[Fact]
        //public async void GetByName_NotExistedProject_ReturnsEmptyCollection()
        //{
        //    var result = await new Projects(_contextFixture.EmptyContext).Get("Proj");
        //    Assert.Empty(result);
        //}

        //[Fact]
        //public async void GetByName_EmptyString_ReturnsAllProjects()
        //{
        //    var result = await new Projects(_contextFixture.Context).Get("");
        //    Assert.NotEmpty(result);
        //}
    }
}
