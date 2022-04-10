using System;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions
{
    internal interface IHandlerCollectionBuilder
    {
        IHandle<T> Handle<T>() where T : Exception;
        IHandle<Exception> HandleByDefault();
        IHandlerCollection BuildCollection();
    }
}
