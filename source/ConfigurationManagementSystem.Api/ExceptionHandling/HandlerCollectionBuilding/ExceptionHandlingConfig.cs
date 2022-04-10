using ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.HandlerCollectionBuilding
{
    internal class ExceptionHandlingConfig
    {
        public IHandlerCollection HandlerCollection { get; set; }
    }
}
