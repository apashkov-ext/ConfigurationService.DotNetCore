using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application.Sources.GitHub.Dto;
using ConfigurationService.Application.Sources.GitHub.Options;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Application.Sources.GitHub
{
    internal class ProjectReader
    {
        private readonly DefaultHttpClient _httpClient;
        private readonly ContentFileNameMatcher _matcher;
        private readonly GitHubApiOptions _options;

        public ProjectReader(DefaultHttpClient httpClient, ContentFileNameMatcher matcher, GitHubApiOptions options)
        {
            _httpClient = httpClient;
            _matcher = matcher;
            _options = options;
        }

        public async Task<Project> GetProject(string name)
        {
            var contents = await _httpClient.GetAsync<ContentDto[]>($"{_options.Repo}/contents?ref={name}");
            var filtered = contents.Where(x => _matcher.IsCorrectName(x.Name));
            var configs = await GetConfigs(name, filtered);
            return Project.Create(new ProjectName(name), configs);
        }

        private async Task<IEnumerable<Configuration>> GetConfigs(string branch, IEnumerable<ContentDto> contents)
        {
            var configs = new List<Configuration>();
            foreach (var c in contents)
            {
                var file = await _httpClient.GetAsync<FileContentDto>($"{_options.Repo}/contents/{c.Name}?ref={branch}");
                var cfg = Configuration.Create(
                    new Env(_matcher.ExtractEnv(file.Name)),
                    new ConfigurationContent(file.Content)
                );
                configs.Add(cfg);
            }

            return configs;
        }
    }
}
