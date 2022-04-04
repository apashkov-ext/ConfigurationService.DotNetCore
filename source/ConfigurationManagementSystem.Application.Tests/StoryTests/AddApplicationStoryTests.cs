using ConfigurationManagementSystem.Api.Tests.Tests;
using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Persistence.StoryImplementations.AddApplicationStory;
using ConfigurationManagementSystem.Persistence.StoryImplementations.GetApplicationByIdStory;
using ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.StoryTests
{
    public class AddApplicationStoryTests : IntegrationTests
    {
        [Fact]
        public void Add_NotExistedApplication_Success()
        {
            const string expectedName = "TestProject";

            ActWithDbContext(ctx =>
            {
                new ContextSetup<ConfigurationManagementSystemContext>(ctx).Initialize();
            });

            ActWithDbContext(async ctx =>
            {
                var story = new AddApplicationStory(
                new GetApplicationByNameQueryEF(ctx),
                new CreateApplicationCommandEF(ctx),
                new GetApplicationByIdWithoutHierarchyQueryEF(ctx));

                var p = await story.ExecuteAsync(expectedName);

                Assert.Equal(p.Name.Value, expectedName);
            });
        }

        [Theory]
        [ClassData(typeof(EmptyApplication))]
        public void Add_ExistedApplication_Exception(ApplicationEntity p)
        {
            ActWithDbContext(ctx =>
            {
                new ContextSetup<ConfigurationManagementSystemContext>(ctx)
                .Initialize()
                .WithEntities(p)
                .Commit();
            });

            ActWithDbContext(async ctx =>
            {
                var story = new AddApplicationStory(
                new GetApplicationByNameQueryEF(ctx),
                new CreateApplicationCommandEF(ctx),
                new GetApplicationByIdWithoutHierarchyQueryEF(ctx));

                await Assert.ThrowsAsync<AlreadyExistsException>(() => story.ExecuteAsync(p.Name.Value));
            });
        }
    }
}
