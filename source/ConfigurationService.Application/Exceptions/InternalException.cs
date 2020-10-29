namespace ConfigurationService.Application.Exceptions
{
    public class InternalException : ApiException
    {
        public InternalException() : base("", 500) { }
        public InternalException(string message) : base(message, 500) { }
    }
}
