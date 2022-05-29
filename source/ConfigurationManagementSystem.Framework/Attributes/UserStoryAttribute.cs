namespace ConfigurationManagementSystem.Framework.Attributes
{
    /// <summary>
    /// Marks class as a Framework Component. 
    /// This allows to register marked class as dependency and resolve it automatically as transient.
    /// </summary>
    public sealed class ComponentAttribute : FrameworkComponentAttribute
    {
    }
}
