using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static void RemoveWithHierarchy(this Configuration env, ConfigurationManagementSystemContext context)
        {
            foreach (var g in env.OptionGroups)
            {
                g.RemoveWithHierarchy(context);
            }

            context.Environments.Remove(env);
        }
    }
}
