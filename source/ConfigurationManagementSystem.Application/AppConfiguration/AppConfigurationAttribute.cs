using System;

namespace ConfigurationManagementSystem.Application.AppConfiguration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AppConfigurationAttribute : Attribute
    {
    }
}