using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConfigurationService.Domain.Entities;

namespace ConfigurationService.Domain
{
    public interface IProjects
    {
        Task<IEnumerable<Project>> GetItems();
    }
}
