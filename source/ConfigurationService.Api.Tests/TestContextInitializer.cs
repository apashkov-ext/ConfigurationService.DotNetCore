using System.Linq;
using ConfigurationService.Persistence;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using System;

namespace ConfigurationService.Api.Tests
{
    internal static class TestContextInitializer
    {
        public static async Task InitializeAsync(ConfigurationServiceContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            context.Projects.Add(project);

            var env = project.AddEnvironment(new Domain.EnvironmentName("Dev"));
            context.Environments.Add(env);

            var group = env.OptionGroups.First();
            context.OptionGroups.Add(group);

            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));
            context.Options.Add(option);

            await context.SaveChangesAsync();
        }
    }
}
