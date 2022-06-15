using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Application.Stories.GetApplicationsStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Tests.Presets;
using Moq;
using System;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.StoryTests;

public class GetApplicationsTests
{
    [Fact]
    public async void Get_Empty_ReturnsEmptyList()
    {
        var geAppByNameQuery = new Mock<IGetApplicationsWithoutHierarchyQuery>();
        geAppByNameQuery.Setup(x => x.ExecuteAsync(It.IsAny<ApplicationName>(), It.IsAny<PaginationOptions>()))
            .ReturnsAsync(() => PagedList<ApplicationEntity>.Empty());

        var story = new FindApplicationsByNameStory(geAppByNameQuery.Object);
        var res = await story.ExecuteAsync(new PagedRequest { Name = "" });

        Assert.NotNull(res);
        Assert.Empty(res.Data);
    }

    [Theory]
    [ClassData(typeof(EmptyApplication))]
    public async void Get_NotEmpty_ReturnsList(ApplicationEntity app)
    {
        var geAppByNameQuery = new Mock<IGetApplicationsWithoutHierarchyQuery>();
        geAppByNameQuery.Setup(x => x.ExecuteAsync(It.IsAny<ApplicationName>(), It.IsAny<PaginationOptions>()))
            .ReturnsAsync(() => PagedList<ApplicationEntity>.Of(app));

        var story = new FindApplicationsByNameStory(geAppByNameQuery.Object);
        var res = await story.ExecuteAsync(new PagedRequest { Name = "" });

        Assert.NotNull(res);
        Assert.NotEmpty(res.Data);
    }

    [Fact]
    public async void Get_NullPaginationArg_ThrowsEx()
    {
        var geAppByNameQuery = new Mock<IGetApplicationsWithoutHierarchyQuery>();
        var story = new FindApplicationsByNameStory(geAppByNameQuery.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => story.ExecuteAsync(null));
    }
}