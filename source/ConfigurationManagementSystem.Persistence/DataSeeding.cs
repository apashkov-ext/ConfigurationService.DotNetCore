using System;
using System.Linq;
using ConfigurationManagementSystem.Application.Stories.SignInStory;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Persistence
{
    internal static class DataSeeding
    {
        public static void Seed(ConfigurationManagementSystemContext context)
        {
            if (context.Applications.Any())
            {
                return;
            }

            var p = ApplicationEntity.Create(new ApplicationName("mars"), new ApiKey(Guid.Parse("22a71687-4249-4a20-8353-02fa6cd70187")));
            var dev = p.AddConfiguration(new ConfigurationName("dev"));
            p.AddConfiguration(new ConfigurationName("last"));
            var devRootGroup = dev.GetRootOptionGroop();

            var nestedGroupLogging = devRootGroup.AddNestedGroup(new OptionGroupName("Logging"));
            nestedGroupLogging.AddOption(new OptionName("loggingEnabled"), new OptionValue(true));
            nestedGroupLogging.AddOption(new OptionName("logErrors"), new OptionValue(true));
            nestedGroupLogging.AddOption(new OptionName("logInfo"), new OptionValue(false));
            nestedGroupLogging.AddOption(new OptionName("dbName"), new OptionValue("MarsLogs"));

            var nestedGroupValidation = devRootGroup.AddNestedGroup(new OptionGroupName("validation"));
            nestedGroupValidation.AddOption(new OptionName("validationEnabled"), new OptionValue(true));
            nestedGroupValidation.AddOption(new OptionName("validationLevel"), new OptionValue(5));
            nestedGroupValidation.AddOption(new OptionName("steps"), new OptionValue(new []{15, 19, 23}));

            var anotherNestedGroup = nestedGroupValidation.AddNestedGroup(new OptionGroupName("sectionValidation"));
            anotherNestedGroup.AddOption(new OptionName("sectionValidatorEnabled"), new OptionValue(true));
            anotherNestedGroup.AddOption(new OptionName("sections"), new OptionValue(new []{"application", "questionnaire"}));

            context.Applications.AddRange(p);

            var user = UserEntity.Create(new Username("Aleksey"), PasswordHasher.HashPassword("Qwerty1"));
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
