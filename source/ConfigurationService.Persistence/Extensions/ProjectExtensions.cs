using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Persistence.Extensions
{
    internal static class ProjectExtensions
    {
        public static void RemoveWithHierarchy(this Project project, ConfigurationServiceContext context)
        {
            foreach (var env in project.Environments)
            {
                env.RemoveWithHierarchy(context);
            }

            context.Projects.Remove(project);
        }
    }
}
