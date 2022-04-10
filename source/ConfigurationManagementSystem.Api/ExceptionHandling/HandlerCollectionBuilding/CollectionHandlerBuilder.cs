using ConfigurationManagementSystem.Api.ExceptionHandling.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationManagementSystem.Api.ExceptionHandling.HandlerCollectionBuilding
{
    internal class HandlerCollectionBuilder : IHandlerCollectionBuilder, IHandlerCollection
    {
        private readonly List<IExceptionHandler> _handlers = new();

        public IHandlerCollection BuildCollection()
        {
            return this;
        }

        public IExceptionHandler FindHandler(Exception ex)
        {
            return _handlers.FirstOrDefault(x => x.CanHandle(ex) || x.CanHandle(new Exception()));
        }

        public IHandle<T> Handle<T>() where T : Exception
        {
            return new ExceptionHandler<T>(this);
        }

        public IHandle<Exception> HandleByDefault()
        {
            return Handle<Exception>();
        }

        public static IHandlerCollectionBuilder Create()
        {
            return new HandlerCollectionBuilder();
        }

        public void AddHandler(IExceptionHandler handler)
        {
            _handlers.Add(handler);
        }
    }
}
