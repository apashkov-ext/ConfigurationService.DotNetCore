﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Application
{
    public interface IOptionGroups
    {
        Task<IEnumerable<OptionGroup>> Get(string name);
        Task<OptionGroup> Get(Guid id);
        Task<OptionGroup> Add(Guid parent, string name);
        Task Update(Guid id, string name);
        Task Remove(Guid id);
    }
}
