using System;
using System.Linq;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Persistence
{
    internal static class DataSeeding
    {
        public static void Seed(ConfigurationServiceContext context)
        {
            if (context.Projects.Any())
            {
                return;
            }

            var p = Project.Create(new ProjectName("mars"), new ApiKey(Guid.Parse("22a71687-4249-4a20-8353-02fa6cd70187")));
            var dev = p.AddEnvironment(new EnvironmentName("dev"));
            p.AddEnvironment(new EnvironmentName("last"));
            var devRootGroup = dev.GetRootOptionGroop();

            var nestedGroupLogging = devRootGroup.AddNestedGroup(new OptionGroupName("Logging"), new Description("Настройки логирования"));
            nestedGroupLogging.AddOption(new OptionName("loggingEnabled"), new Description("Логирование включено"), new OptionValue(true));
            nestedGroupLogging.AddOption(new OptionName("logErrors"), new Description("Логировать ошибки"), new OptionValue(true));
            nestedGroupLogging.AddOption(new OptionName("logInfo"), new Description("Логировать информационные сообщения"), new OptionValue(false));
            nestedGroupLogging.AddOption(new OptionName("dbName"), new Description("База данных для логирования"), new OptionValue("MarsLogs"));

            var nestedGroupValidation = devRootGroup.AddNestedGroup(new OptionGroupName("validation"), new Description("Настройки валидации"));
            nestedGroupValidation.AddOption(new OptionName("validationEnabled"), new Description("Валидация включена"), new OptionValue(true));
            nestedGroupValidation.AddOption(new OptionName("validationLevel"), new Description("Уровень вложенности валидации"), new OptionValue(5));
            nestedGroupValidation.AddOption(new OptionName("steps"), new Description("Валидация активна на этапах"), new OptionValue(new []{15, 19, 23}));

            var anotherNestedGroup = nestedGroupValidation.AddNestedGroup(new OptionGroupName("sectionValidation"), new Description("Валидация секций"));
            anotherNestedGroup.AddOption(new OptionName("sectionValidatorEnabled"), new Description("Валидация секций включена"), new OptionValue(true));
            anotherNestedGroup.AddOption(new OptionName("sections"), new Description("Валидировать указанные секции"), 
                new OptionValue(new []{"application", "questionnaire"}));

            context.Projects.AddRange(p);
            context.SaveChanges();
        }
    }
}
