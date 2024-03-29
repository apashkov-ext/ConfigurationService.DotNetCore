﻿using ConfigurationManagementSystem.Framework.Attributes;
using ConfigurationManagementSystem.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationsStory
{
    [Component]
    public abstract class GetApplicationsWithHierarchyQuery
    {
        public abstract Task<IQueryable<ApplicationEntity>> ExecuteAsync();
    }
}
