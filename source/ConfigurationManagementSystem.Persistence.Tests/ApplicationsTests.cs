using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence.StoryImplementations.AddApplicationStory;
using ConfigurationManagementSystem.Tests;
using ConfigurationManagementSystem.Tests.Fixtures;
using ConfigurationManagementSystem.Tests.Presets;
using Moq;
using System;
using Xunit;

namespace ConfigurationManagementSystem.Persistence.Tests
{
    public class GetApplicationByNameQueryTests : IntegrationTests
    {
        [Fact]
        public async void Execute_NotExisted_ReturnsNull()
        {
            var expectedName = new ApplicationName("TestApplication");
            var getAppByNameQueryMock = new Mock<GetApplicationByNameQuery>();
            using var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
            
            var app = await new GetApplicationByNameQueryEF(ctx).ExecuteAsync(expectedName);

            Assert.Null(app);
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public async void Execute_Existed_ReturnsExisted(ApplicationEntity app)
        {
            var getAppByNameQueryMock = new Mock<GetApplicationByNameQuery>();
            using var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, app)).Context;

            var actual = await new GetApplicationByNameQueryEF(ctx).ExecuteAsync(new ApplicationName(app.Name.Value));

            Assert.Equal(app, actual);
        }
    }

    //public class CreateApplicationCommandTests
    //{
    //    [Fact]
    //    public async void Execute_NotExisted_ReturnsNull()
    //    {
    //        var expectedName = new ApplicationName("TestApplication");
    //        var getAppByNameQueryMock = new Mock<GetApplicationByNameQuery>();
    //        using var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;

    //        var app = await new CreateApplicationCommandEF(ctx).ExecuteAsync(expectedName, new ApiKey(Guid.NewGuid()));

    //        Assert.Equal(expectedName, app.);
    //    }
    //}

    public class ApplicationsTests
    {
        //[Fact]
        //public async void Add_NotExistedApplication_Success()
        //{
        //    const string expectedName = "TestApplication";
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
        //    var story = new AddApplicationStory(
        //        new GetApplicationByNameQueryEF(ctx),
        //        new CreateApplicationCommandEF(ctx),
        //        new GetApplicationByIdWithoutHierarchyQueryEF(ctx)
        //        );
            
        //    var app = await story.ExecuteAsync(expectedName);
        //    Assert.Equal(app.Name.Value, expectedName);
        //}

        //[Fact]
        //public async void Get_ExistedApplication_Success()
        //{
        //    const string expectedName = "TestApplication";

        //    var getAppByNameQueryMock = new Mock<GetApplicationByNameQuery>();
        //    getAppByNameQueryMock
        //        .Setup(x => x.ExecuteAsync(new Domain.ValueObjects.ApplicationName(expectedName)))
        //        .ReturnsAsync();

        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
        //    var story = new AddApplicationStory(
        //        new GetApplicationByNameQueryEF(ctx),
        //        new CreateApplicationCommandEF(ctx),
        //        new GetApplicationByIdWithoutHierarchyQueryEF(ctx)
        //        );

        //    var app = await story.ExecuteAsync(expectedName);
        //    Assert.Equal(app.Name.Value, expectedName);
        //}

        //[Theory]
        //[ClassData(typeof(EmptyApplication))]
        //public async void Add_ExistedApplication_Exception(ApplicationEntity p)
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
        //    await Assert.ThrowsAsync<AlreadyExistsException>(() => new Applications(ctx).AddAsync(p.Name.Value));
        //}

        //[Theory]
        //[ClassData(typeof(EmptyApplication))]
        //public async void Remove_ExistedApplication_Success(ApplicationEntity p)
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
        //    await new Applications(ctx).RemoveAsync(p.Id);
        //}

        //[Fact]
        //public async void Remove_NotExistedApplication_Exception()
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
        //    await Assert.ThrowsAsync<EntityNotFoundException>(() => new Applications(ctx).RemoveAsync(Guid.NewGuid()));
        //}

        //[Theory]
        //[ClassData(typeof(EmptyApplication))]
        //public async void GetById_ExistedApplication_Success(ApplicationEntity p)
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
        //    var proj = await new Applications(ctx).GetAsync(p.Id);
        //    Assert.Equal(proj, p);
        //}

        //[Fact]
        //public async void GetById_NotExistedApplication_Exception()
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
        //    await Assert.ThrowsAsync<EntityNotFoundException>(() => new Applications(ctx).GetAsync(Guid.NewGuid()));
        //}

        //[Theory]
        //[ClassData(typeof(EmptyApplication))]
        //public async void GetByName_ExistedApplication_ReturnsCollectionWithTheApplication(ApplicationEntity p)
        //{
        //    const string search = "aPPlicaTion";
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
        //    var result = await new Applications(ctx).GetAsync(search, GetPaginationOptions());
        //    Assert.Contains(result.Data, x => x.Name.Value.StartsWith(search, StringComparison.InvariantCultureIgnoreCase));
        //}

        //[Fact]
        //public async void GetByName_NotExistedApplication_ReturnsEmptyCollection()
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications)).Context;
        //    var result = await new Applications(ctx).GetAsync("TestApplication", GetPaginationOptions());
        //    Assert.Empty(result.Data);
        //}

        //[Theory]
        //[ClassData(typeof(EmptyApplication))]
        //public async void GetByName_EmptyString_ReturnsAllApplications(ApplicationEntity p)
        //{
        //    var ctx = new DbContextFixture(x => x.WithSet(s => s.Applications, p)).Context;
        //    var result = await new Applications(ctx).GetAsync("", GetPaginationOptions());
        //    Assert.NotEmpty(result.Data);
        //}

        private static PaginationOptions GetPaginationOptions()
        {
            return new PaginationOptions(0, 100);
        }
    }
}
