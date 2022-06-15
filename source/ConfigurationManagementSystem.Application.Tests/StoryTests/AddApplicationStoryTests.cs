using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.StoryTests;

public class AddApplicationStoryTests
{
    [Theory]
    [ClassData(typeof(EmptyApplication))]
    public async Task Add_NotExistedApplication_Success(ApplicationEntity app)
    {
        var getAppByNameQueryMock = new Mock<GetApplicationByNameQuery>();
        getAppByNameQueryMock.Setup(x => x.ExecuteAsync(It.IsAny<ApplicationName>())).ReturnsAsync(() => null);

        var createAppCommand = new Mock<CreateApplicationCommand>();

        var getAppByIdQueryMock = new Mock<GetApplicationByIdWithoutHierarchyQuery>();
        getAppByIdQueryMock.Setup(x => x.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync(() => app);
        var story = new AddApplicationStory(getAppByNameQueryMock.Object, createAppCommand.Object, getAppByIdQueryMock.Object);

        var p = await story.ExecuteAsync("NewApp");

        Assert.NotNull(p);
    }

    [Theory]
    [ClassData(typeof(EmptyApplication))]
    public async Task Add_ExistedApplication_ThrowsException(ApplicationEntity app)
    {
        var getAppByNameQueryMock = new Mock<GetApplicationByNameQuery>();
        getAppByNameQueryMock.Setup(x => x.ExecuteAsync(It.IsAny<ApplicationName>())).ReturnsAsync(() => app);
        var createAppCommand = new Mock<CreateApplicationCommand>();
        var getAppByIdQueryMock = new Mock<GetApplicationByIdWithoutHierarchyQuery>();
        var story = new AddApplicationStory(getAppByNameQueryMock.Object, createAppCommand.Object, getAppByIdQueryMock.Object);

        await Assert.ThrowsAsync<AlreadyExistsException>(() => story.ExecuteAsync(app.Name.Value));
    }
}
