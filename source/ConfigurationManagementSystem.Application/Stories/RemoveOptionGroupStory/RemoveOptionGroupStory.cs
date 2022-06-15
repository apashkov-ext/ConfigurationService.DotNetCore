using ConfigurationManagementSystem.Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Application.Stories.RemoveOptionGroupStory
{
    [Component]
    public class RemoveOptionGroupStory
    {
        public Task ExecuteAsync(Guid id)
        {
            return Task.CompletedTask;
        }
    }
}
