using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Application.Stories.FindOptionGroupsByNameStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Moq;
using System;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.StoryTests;

public class FindOptionGroupsByNameStoryTests
{
    [Fact]
    public async void Get_Empty_ReturnsEmptyList()
    {
        var geAppByNameQuery = new Mock<IGetOptionGroupsByNameQuery>();
        geAppByNameQuery.Setup(x => x.ExecuteAsync(It.IsAny<OptionGroupName>(), It.IsAny<PaginationOptions>()))
            .ReturnsAsync(() => PagedList<OptionGroupEntity>.Empty());
        var story = new FindOptionGroupsByNameStory(geAppByNameQuery.Object);

        var res = await story.ExecuteAsync(new PagedRequest());

        Assert.NotNull(res);
        Assert.Empty(res.Data);
    }

    [Theory]
    [ClassData(typeof(NonRootOptionGroup))]
    public async void Get_NotEmpty_ReturnsList(OptionGroupEntity group)
    {
        var geAppByNameQuery = new Mock<IGetOptionGroupsByNameQuery>();
        geAppByNameQuery.Setup(x => x.ExecuteAsync(It.IsAny<OptionGroupName>(), It.IsAny<PaginationOptions>()))
            .ReturnsAsync(() => PagedList<OptionGroupEntity>.Of(group));
        var story = new FindOptionGroupsByNameStory(geAppByNameQuery.Object);

        var res = await story.ExecuteAsync(new PagedRequest());

        Assert.NotNull(res);
        Assert.NotEmpty(res.Data);
    }

    [Fact]
    public async void Get_PassNullPaginationOptions_ThrowsEx()
    {
        var geAppByNameQuery = new Mock<IGetOptionGroupsByNameQuery>();
        var story = new FindOptionGroupsByNameStory(geAppByNameQuery.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => story.ExecuteAsync(null));
    }
}
