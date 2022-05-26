using ConfigurationManagementSystem.Application.Exceptions;
using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Persistence.StoryImplementations.AddApplicationStory;
using ConfigurationManagementSystem.Persistence.StoryImplementations.GetApplicationByIdStory;
using ConfigurationManagementSystem.Tests;
using ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization;
using ConfigurationManagementSystem.Tests.Presets;
using Xunit;

namespace ConfigurationManagementSystem.Application.Tests.StoryTests
{
    public class AddApplicationStoryTests : IntegrationTests
    {
        public AddApplicationStoryTests()
        {
        }

        [Fact]
        public void Add_NotExistedApplication_Success()
        {
            const string expectedName = "TestProject";

            ActWithDbContext(ctx =>
            {
                new ContextPreparation<ConfigurationManagementSystemContext>(ctx).Setup();
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
                new ContextPreparation<ConfigurationManagementSystemContext>(ctx)
                .Setup()
                .WithEntities(p)
                .Build();
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
