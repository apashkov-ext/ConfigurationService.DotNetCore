using System;

namespace ConfigurationManagementSystem.Application.Stories.Framework
{
    /// <summary>
    /// Marks class as a User Story thing. 
    /// User story implements a single business value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class UserStoryAttribute : Attribute
    {
    }
}
