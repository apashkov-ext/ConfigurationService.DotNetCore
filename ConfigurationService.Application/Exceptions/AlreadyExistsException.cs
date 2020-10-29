namespace ConfigurationService.Application.Exceptions
{
    public class AlreadyExistsException : ApiException
    {
        public AlreadyExistsException() : base("", 422) { }
        public AlreadyExistsException(string message) : base(message, 422) { }
    }
}
