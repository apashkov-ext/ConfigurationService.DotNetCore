using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Application.Stories.GetApplicationsStory;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Persistence.StoryImplementations
{
    public class GetApplicationsWithoutHierarchyQueryEF : IGetApplicationsWithoutHierarchyQuery
    {
        private readonly ConfigurationManagementSystemContext _context;

        public GetApplicationsWithoutHierarchyQueryEF(ConfigurationManagementSystemContext context)
        {
            _context = context;
        }

        public Task<PagedList<ApplicationEntity>> ExecuteAsync(ApplicationName filterByName, PaginationOptions paginationOptions)
        {
            var query = filterByName == null
                ? _context.Applications.AsNoTrackingWithIdentityResolution()
                : _context.Applications.Where(x => x.Name.Value.StartsWith(filterByName.Value, StringComparison.OrdinalIgnoreCase)).AsNoTrackingWithIdentityResolution();

            return query.AsPagedListAsync(paginationOptions);
        }
    }
}
