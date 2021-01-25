using System;
using System.Collections.Generic;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Domain.ValueObjects.OptionValueTypes;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence
{
    internal static class DataSeeding
    {
        public static void Seed(ConfigurationServiceContext context)
        {
            var options = new List<Option>();
            var groups = new List<OptionGroup>();

            var rootGroup = OptionGroup.Create(new OptionGroupName(""), new Description(""), new List<Option>(), null, new List<OptionGroup>());
            groups.Add(rootGroup);

            var nestedGroupLogging = OptionGroup.Create(
                new OptionGroupName("Logging"), 
                new Description("Настройки логирования"), 
                new List<Option>(), 
                rootGroup, 
                new List<OptionGroup>());
            groups.Add(nestedGroupLogging);
            rootGroup.AddNestedGroup(nestedGroupLogging);
            nestedGroupLogging.AddOption(Option.Create(new OptionName("loggingEnabled"), new Description("Логирование включено"), new BooleanValue(true),  nestedGroupLogging));
            nestedGroupLogging.AddOption(Option.Create(new OptionName("logErrors"), new Description("Логировать ошибки"), new BooleanValue(true), nestedGroupLogging));
            nestedGroupLogging.AddOption(Option.Create(new OptionName("logInfo"), new Description("Логировать информационные сообщения"), new BooleanValue(false), nestedGroupLogging));
            nestedGroupLogging.AddOption(Option.Create(new OptionName("dbName"), new Description("База данных для логирования"), new StringValue("MarsLogs"), nestedGroupLogging));
            options.AddRange(nestedGroupLogging.Options);

            var nestedGroupValidation = OptionGroup.Create(
                new OptionGroupName("validation"),
                new Description("Настройки валидации"),
                new List<Option>(),
                rootGroup,
                new List<OptionGroup>());
            groups.Add(nestedGroupValidation);
            rootGroup.AddNestedGroup(nestedGroupValidation);
            nestedGroupValidation.AddOption(Option.Create(new OptionName("validationEnabled"), new Description("Валидация включена"), new BooleanValue(true),  nestedGroupValidation));
            nestedGroupValidation.AddOption(Option.Create(new OptionName("validationLevel"), new Description("Уровень вложенности валидации"), new NumberValue(5),  nestedGroupValidation));
            nestedGroupValidation.AddOption(Option.Create(new OptionName("steps"), new Description("Валидация активна на этапах"), new NumberArrayValue(new []{15, 19, 23}), nestedGroupValidation));
            options.AddRange(nestedGroupValidation.Options);

            var anotherNestedGroup = OptionGroup.Create(
                new OptionGroupName("sectionValidation"),
                new Description("Валидация секций"),
                new List<Option>(),
                nestedGroupValidation,
                new List<OptionGroup>());
            groups.Add(anotherNestedGroup);
            nestedGroupValidation.AddNestedGroup(anotherNestedGroup);
            anotherNestedGroup.AddOption(Option.Create(new OptionName("sectionValidatorEnabled"), new Description("Валидация секций включена"), new BooleanValue(true), anotherNestedGroup));
            anotherNestedGroup.AddOption(Option.Create(
                new OptionName("sections"), 
                new Description("Валидировать указанные секции"), 
                new StringArrayValue(new []{"application", "questionnaire"}),
                anotherNestedGroup));

            options.AddRange(anotherNestedGroup.Options);

            var p = Project.Create(new ProjectName("mars"), new ApiKey(Guid.Parse("22a71687-4249-4a20-8353-02fa6cd70187")), new List<Environment>());
            var dev = Environment.Create(new EnvironmentName("dev"), p, true, rootGroup);
            var last = Environment.Create(new EnvironmentName("last"), p, false, OptionGroup.Create(new OptionGroupName(""), new Description(""), new List<Option>(), null, new List<OptionGroup>()));

            p.AddEnvironment(dev);
            p.AddEnvironment(last);

            groups.Add(nestedGroupLogging);
            context.OptionGroups.AddRange(groups);

            context.Projects.AddRange(p);
            context.Environments.AddRange(dev, last);

            context.Options.AddRange(options);

            context.SaveChanges();
        }
    }
}
