namespace ConfigurationManagementSystem.Framework.Bootstrap.ComponentScanning
{
    internal interface ITypeProvider
    {
        IEnumerable<Type> GetTypes();
    }
}
