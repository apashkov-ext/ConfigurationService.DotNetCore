namespace ConfigurationService.Application.Sources.GitHub.Options
{
    public class GitHubApiOptions
    {
        public const string Path = "Sources:GitHub";

        public string Uri { get; set; }
        public string Repo { get; set; }
        public string UserName { get; set; }
        public string PersonalToken { get; set; }
    }
}
