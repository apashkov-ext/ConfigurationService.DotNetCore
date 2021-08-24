using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static void RemoveEnvironmentWithHierarchy(this Environment env, ConfigurationServiceContext context)
        {
            foreach (var g in env.OptionGroups)
            {
                g.RemoveOptionGroupWithHierarchy(context);
            }

            context.Environments.Remove(env);
        }
    }
}
