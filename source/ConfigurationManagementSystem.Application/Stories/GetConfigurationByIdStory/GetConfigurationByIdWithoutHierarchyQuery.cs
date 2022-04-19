﻿using ConfigurationManagementSystem.Application.Stories.Framework;
using ConfigurationManagementSystem.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory
{
    [Query]
    public abstract class GetConfigurationByIdWithoutHierarchyQuery
    {
        public abstract Task<ConfigurationEntity> ExecuteAsync(Guid id);
    }
}
