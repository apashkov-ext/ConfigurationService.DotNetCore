namespace ConfigurationManagementSystem.Api.Dto
{
    public class GetRequestOptions
    {
        public string Name { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
