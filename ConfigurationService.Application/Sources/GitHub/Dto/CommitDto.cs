namespace ConfigurationService.Application.Sources.GitHub.Dto 
{
    public class CommitDto 
    {
        public string Sha { get; set; }
        public TreeDto Tree { get; set; }
    }
}