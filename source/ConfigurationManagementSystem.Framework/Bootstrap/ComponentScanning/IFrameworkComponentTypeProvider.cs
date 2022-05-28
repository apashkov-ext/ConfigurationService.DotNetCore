using ConfigurationManagementSystem.Framework.Attributes;

namespace ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning
{
    internal interface IFrameworkComponentTypeProvider
    {
        IEnumerable<Type> GetComponentTypesByAttribute<T>() where T : FrameworkComponentAttribute;
    }
}
