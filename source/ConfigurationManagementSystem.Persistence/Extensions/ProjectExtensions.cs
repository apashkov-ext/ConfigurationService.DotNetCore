using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Persistence.Extensions
{
    internal static class ProjectExtensions
    {
        public static void RemoveWithHierarchy(this Project project, ConfigurationManagementSystemContext context)
        {
            foreach (var env in project.Environments)
            {
                env.RemoveWithHierarchy(context);
            }

            context.Projects.Remove(project);
        }
    }
}
