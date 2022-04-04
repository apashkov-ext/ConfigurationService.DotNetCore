﻿using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory
{
    [Query]
    public abstract class GetApplicationByIdWithHierarchyQuery
    {
        public abstract Task<ApplicationEntity> ExecuteAsync(Guid id);
    }
}
