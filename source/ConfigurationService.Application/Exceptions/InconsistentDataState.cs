namespace ConfigurationService.Application.Exceptions
{
    public class InconsistentDataState : ApiException
    {
        public InconsistentDataState() : base("", 422) { }
        public InconsistentDataState(string message) : base(message, 422) { }
    }
}
