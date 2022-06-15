using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Tests.Presets;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.StoryTests;

public class GetApplicationByIdStoryTests
{
    [Fact]
    public async Task Get_NotExistedApplication_ThrowsException()
    {
        var getAppByIdMock = new Mock<GetApplicationByIdWithoutHierarchyQuery>();
        getAppByIdMock.Setup(x => x.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

        var story = new GetApplicationByIdStory(getAppByIdMock.Object);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => story.ExecuteAsync(Guid.NewGuid()));
    }

    [Theory]
    [ClassData(typeof(EmptyApplication))]
    public async Task Get_ExistedApplication_ReturnsApp(ApplicationEntity app)
    {
        var getAppByIdMock = new Mock<GetApplicationByIdWithoutHierarchyQuery>();
        getAppByIdMock.Setup(x => x.ExecuteAsync(It.IsAny<Guid>())).ReturnsAsync(() => app);

        var story = new GetApplicationByIdStory(getAppByIdMock.Object);
        var ent = await story.ExecuteAsync(Guid.NewGuid());

        Assert.NotNull(ent);
    }
}
