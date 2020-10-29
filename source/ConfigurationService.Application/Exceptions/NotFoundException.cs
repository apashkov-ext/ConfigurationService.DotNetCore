namespace ConfigurationService.Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException() : base("", 404) { }
        public NotFoundException(string message) : base(message, 404) { }
    }
}
