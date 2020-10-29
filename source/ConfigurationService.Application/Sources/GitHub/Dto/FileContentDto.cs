namespace ConfigurationService.Application.Sources.GitHub.Dto
{
    internal class FileContentDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string Sha { get; set; }
        public string Content { get; set; }
    }
}
