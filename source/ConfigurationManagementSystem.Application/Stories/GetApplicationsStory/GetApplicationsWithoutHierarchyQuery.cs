﻿using ConfigurationManagementSystem.Application.Stories;
using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationsStory
{
    [Query]
    public abstract class GetApplicationsWithoutHierarchyQuery
    {
        public abstract Task<IQueryable<ApplicationEntity>> ExecuteAsync();
    }
}
