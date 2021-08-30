using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static void RemoveWithHierarchy(this Environment env, ConfigurationServiceContext context)
        {
            foreach (var g in env.OptionGroups)
            {
                g.RemoveWithHierarchy(context);
            }

            context.Environments.Remove(env);
        }
    }
}
