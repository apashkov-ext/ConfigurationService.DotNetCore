namespace ConfigurationService.Application.Sources.GitHub.Dto
{
    public class BranchDto
    {
        public string Name { get; set; }
        public CommitDto Commit { get; set; }
    }
}
