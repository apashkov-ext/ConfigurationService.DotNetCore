using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Application.Sources.GitHub.Dto;
using ConfigurationService.Application.Sources.GitHub.Options;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace ConfigurationService.Application.Sources.GitHub
{
    public class GitHubApi : ISourceApi
    {
        private readonly ContentFileNameMatcher _matcher = new ContentFileNameMatcher("configuration");
        private readonly DefaultHttpClient _httpClient;
        private readonly GitHubApiOptions _options;

        public GitHubApi(DefaultHttpClient httpClient, IOptions<GitHubApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var repo = await _httpClient.GetAsync<RepoDto>(_options.Repo);
            var branches = await _httpClient.GetAsync<BranchDto[]>($"{_options.Repo}/branches");
            var filtered = branches.Where(x => x.Name != repo.default_branch);

            var projects = new List<Project>();
            var reader = new ProjectReader(_httpClient, _matcher, _options);
            foreach (var b in filtered)
            {
                projects.Add(await reader.GetProject(b.Name));
            }

            return projects;
        }

        public async Task<Project> GetProject(string name)
        {
            var branch = await FindBranch(name);
            if (branch == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            return await new ProjectReader(_httpClient, _matcher, _options).GetProject(name);
        }

        public async Task<Project> CreateProjects(string name)
        {
            var r = await FindRef(name);
            if (r != null)
            {
                throw new AlreadyExistsException("Project with the same name already exists");
            }

            var repo = await _httpClient.GetAsync<RepoDto>(_options.Repo);
            var defaultRef = await _httpClient.GetAsync<RefDto>($"{_options.Repo}/git/refs/heads/{repo.default_branch}");
            var req = new NewRefDto
            {
                Ref = $"refs/heads/{name}",
                Sha = defaultRef.Object.Sha
            };

            // Начиная отсюда можно оптимизировать. Не запрашивать, а создавать все тут же. Только создать ref.
            var newRef = await _httpClient.PostAsync<RefDto>("{_options.Repo}/git/refs", req);
            var branch = await _httpClient.GetAsync<BranchDto>($"{_options.Repo}/branches/{newRef.Ref.Replace("refs/heads/", string.Empty)}");
            return await new ProjectReader(_httpClient, _matcher, _options).GetProject(branch.Name);
        }

        public async Task DeleteProject(string name)
        {
            var r = await FindRef(name);
            if (r == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            await _httpClient.DeleteAsync($"{_options.Repo}/git/refs/heads/{name}");
        }

        public async Task<Configuration> GetConfig(string projectName, string environment)
        {
            var project = await GetProject(projectName);
            var config = project.Configurations.FirstOrDefault(x => x.Environment.Value == environment);
            if (config == null)
            {
                throw new NotFoundException("Configuration does not exist");
            }

            return config;
        }

        public async Task<Configuration> CreateConfig(string projectName, string environment)
        {
            var branch = await FindBranch(projectName);
            if (branch == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            var newConfigName = _matcher.BuildFileName(environment);
            var existed = await FindContent(branch.Name, newConfigName);
            if (existed != null)
            {
                throw new AlreadyExistsException("Configuration for this environment already exists");
            }

            var defaultConfig = await GetDefaultConfig(branch.Name);
            if (defaultConfig == null)
            {
                throw new InternalException("Default configuration is not available");
            }

            var request = new
            {
                message = "Create file[${ filePath}]",
                branch = branch.Name,
                content = defaultConfig.Content
            };
            await _httpClient.PutAsync<FileDto>($"{_options.Repo}/contents/{newConfigName}", request);

            return Configuration.Create(new Env(environment), new ConfigurationContent(defaultConfig.Content));
        }

        public async Task UpdateConfig(string projectName, string environment, string content)
        {
            var fileName = _matcher.BuildFileName(environment);
            var file = await FindContent(projectName, fileName);
            if (file == null)
            {
                throw new NotFoundException("Configuration does not exist");
            }

            var request = new
            {
                message = $"Update file[${fileName}]",
                sha = file.Sha,
                branch = projectName,
                content
            };

            await _httpClient.PutAsync<FileDto>($"{_options.Repo}/contents/{fileName}", request);
        }

        public async Task DeleteConfig(string projectName, string environment)
        {
            var fileName = _matcher.BuildFileName(environment);
            var file = await FindContent(projectName, fileName);
            if (file == null)
            {
                throw new NotFoundException("Configuration does not exist");
            }

            var request = new
            {
                message = $"Update file[${fileName}]",
                sha = file.Sha,
                branch = projectName
            };
            await _httpClient.DeleteAsync($"{_options.Repo}/contents/{fileName}", request);
        }

        private async Task<RefDto> FindRef(string name)
        {
            var matched = await _httpClient.GetAsync<RefDto[]>($"{_options.Repo}/git/matching-refs/heads/{name}");
            return matched.FirstOrDefault(x => x.Ref.EndsWith($"/{name}"));
        }

        private async Task<BranchDto> FindBranch(string name)
        {
            try
            {
                return await _httpClient.GetAsync<BranchDto>($"{_options.Repo}/branches/{name}");
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        private async Task<FileContentDto> FindContent(string branch, string fileName)
        {
            try
            {
                return await _httpClient.GetAsync<FileContentDto>($"{_options.Repo}/contents/${fileName}?ref=${branch}");
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        private async Task<FileContentDto> GetDefaultConfig(string branch)
        {
            var defaultConfigName = _matcher.BuildFileName();
            var defaultConfig = await FindContent(branch, defaultConfigName);
            if (defaultConfig != null)
            {
                return defaultConfig;
            }

            var repo = await _httpClient.GetAsync<RepoDto>(_options.Repo);
            if (repo.default_branch == branch)
            {
                return null;
            }
            
            return await FindContent(repo.default_branch, defaultConfigName);
        }
    }
}
