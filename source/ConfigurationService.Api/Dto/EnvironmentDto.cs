namespace ConfigurationService.Api.Dto
{
    public class EnvironmentDto
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public OptionGroupDto OptionGroup { get; set; }
        public string Preview { get; set; }
    }
}