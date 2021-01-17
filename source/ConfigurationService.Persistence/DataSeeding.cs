using System;
using System.Collections.Generic;
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

            var p = Project.Create(new Name("mars"), new ApiKey(Guid.NewGuid()), envs);
            var group = OptionGroup.Create(new Name(""), new Description(""), options, null, new List<OptionGroup>());
            var env = Environment.Create(new Name("dev"), p, true, group);
            var o = Option.Create(new Name("loggingEnabled"), new Description(""), new OptionValue(true), OptionValueType.String, group);

            options.Add(o);
            envs.Add(env);
            groups.Add(group);
            projects.Add(p);

            context.Projects.AddRange(projects);
            context.Environments.AddRange(envs);
            context.OptionGroups.AddRange(groups);
            context.Options.AddRange(options);

            context.SaveChanges();
        }
    }
}
