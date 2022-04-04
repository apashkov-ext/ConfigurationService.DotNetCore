using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class ProjectExtensions
    {
        public static void RemoveWithHierarchy(this Domain.Entities.ApplicationEntity project, ConfigurationManagementSystemContext context)
        {
            foreach (var env in project.Configurations)
            {
                env.RemoveWithHierarchy(context);
            }

            context.Applications.Remove(project);
        }
    }
}
