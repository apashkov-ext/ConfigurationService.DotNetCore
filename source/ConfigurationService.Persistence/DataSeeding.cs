using System;
using System.Collections.Generic;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence
{
    internal static class DataSeeding
    {
        public static void Seed(ConfigurationServiceContext context)
        {
            var options = new List<Option>();
            var envs = new List<Environment>();
            var groups = new List<OptionGroup>();
            var projects = new List<Project>();

            var p = Project.Create(new ProjectName("mars"), new ApiKey(Guid.Parse("22a71687-4249-4a20-8353-02fa6cd70187")), envs);
            var rootGroup = OptionGroup.Create(new OptionGroupName(""), new Description(""), new List<Option>(), null, new List<OptionGroup>());
            var nestedGroup = OptionGroup.Create(new OptionGroupName("Logging"), new Description("Настройки логирования"), options, rootGroup, new List<OptionGroup>());
            rootGroup.AddNestedGroup(nestedGroup);
            options.AddRange(new List<Option>
            {
                Option.Create(new OptionName("loggingEnabled"), new Description("Логирование включено"), new OptionValue(true), OptionValueType.Boolean, nestedGroup),
                Option.Create(new OptionName("logErrors"), new Description("Логировать ошибки"), new OptionValue(true), OptionValueType.Boolean, nestedGroup),
                Option.Create(new OptionName("logInfo"), new Description("Логировать информационные сообщения"), new OptionValue(false), OptionValueType.Boolean, nestedGroup),
                Option.Create(new OptionName("dbName"), new Description("База данных для логирования"), new OptionValue("MarsLogs"), OptionValueType.String, nestedGroup)
            });
            envs.AddRange(new List<Environment>
            {
                Environment.Create(new EnvironmentName("dev"), p, true, rootGroup)
            });

            groups.Add(rootGroup);
            groups.Add(nestedGroup);
            projects.Add(p);

            context.Projects.AddRange(projects);
            context.Environments.AddRange(envs);
            context.OptionGroups.AddRange(groups);
            context.Options.AddRange(options);

            context.SaveChanges();
        }
    }
}
