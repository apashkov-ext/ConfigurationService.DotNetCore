namespace ConfigurationService.Application.Sources.GitHub.Dto
{
    public class FileDto
    {
        public ContentDto Content { get; set; }
        public CommitDto Commit { get; set; }
    }
}