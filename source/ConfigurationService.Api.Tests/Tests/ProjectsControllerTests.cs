using Xunit;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationService.Persistence;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain;
using ConfigurationService.Persistence.ContextInitialization;
using ConfigurationService.Api.Tests.DtoAssertions;
using ConfigurationService.Api.Tests.Extensions;

namespace ConfigurationService.Api.Tests.Tests
{
    public class ProjectsControllerTests : IClassFixture<WebAppFactory>
    {
        private readonly WebAppFactory _webAppFactory;
        private readonly HttpClient _httpClient;

        public ProjectsControllerTests(WebAppFactory webAppFactory)
        {
            _webAppFactory = webAppFactory;
            _httpClient = webAppFactory.CreateClient();
        }

        [Fact]
        public async void GetAll_NotExists_ReturnsEmptyArray()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<ProjectDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(actual);
        }

        [Fact]
        public async void GetAll_ExistsSingleWithoutHierarchy_ReturnsSingleWithoutHierarchy()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<ProjectDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual);
            Assertions.ProjectDtosAreEquivalentToModel(actual, project);
        }

        [Fact]
        public async void GetAll_ExistsSingleWithHierarchy_ReturnsSingleWithHierarchy()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .WithEntities(env)
                .WithEntities(group)
                .WithEntities(option)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<ProjectDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual);
            Assertions.ProjectDtosAreEquivalentToModel(actual, project);
        }

        [Fact]
        public async void GetById_NotExists_Returns404()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects/{Guid.NewGuid()}");
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void GetById_Exists_ReturnsWithHierarchy()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .WithEntities(env)
                .WithEntities(group)
                .WithEntities(option)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects/{project.Id}");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<ProjectDto>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assertions.ProjectDtoIsEquivalentToModel(actual, project);
        }

        [Fact]
        public async void Post_Exists_Returns422()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .Save();

            var body = new
            {
                name = "TestProject"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/projects")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async void Post_NotExists_Returns201AndCorrectResponse()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();

            var body = new
            {
                name = "TestProject"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/projects")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<CreatedProjectDto>();

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(body.name, actual.Name);
        }

        [Fact]
        public async void Delete_NotExists_Returns404()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{Guid.NewGuid()}");
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Delete_ExistsWithNoHierarchy_ReturnsNoContent()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void Delete_ExistsWithFullHierarchy_ReturnsNoContent()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .WithEntities(env)
                .WithEntities(group)
                .WithEntities(option)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void Delete_Exists_RemovesEntityFromContext()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));

            var scopeFactory = _webAppFactory.Services;
            using (var scp = scopeFactory.CreateScope())
            {
                var cont = scp.ServiceProvider.GetService<ConfigurationServiceContext>();
                new ContextSetup<ConfigurationServiceContext>(cont)
                    .Initialize()
                    .WithEntities(project)
                    .Save();
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            _ = await _httpClient.SendAsync(request);

            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
            var projects = context.Projects.ToList();

            Assert.Empty(projects);
        }

        [Fact]
        public async void Delete_ExistsWithFullHierarchy_RemovesAllDependentEntities()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));

            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));

            var nestedGroup = group.AddNestedGroup(new OptionGroupName("Nested"), new Description("Some nested froup"));
            var nestedOption = nestedGroup.AddOption(new OptionName("SomeOpt"), new Description("Some nested option"), new OptionValue(888));

            var scopeFactory = _webAppFactory.Services;
            using (var scp = scopeFactory.CreateScope())
            {
                var cont = scp.ServiceProvider.GetService<ConfigurationServiceContext>();
                new ContextSetup<ConfigurationServiceContext>(cont)
                    .Initialize()
                    .WithEntities(project)
                    .WithEntities(env)
                    .WithEntities(group, nestedGroup)
                    .WithEntities(option, nestedOption)
                    .Save();
            }

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            _ = await _httpClient.SendAsync(request);

            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
            var environments = context.Environments.ToList();
            var groups = context.OptionGroups.ToList();
            var options = context.Options.ToList();

            Assert.Empty(environments);
            Assert.Empty(groups);
            Assert.Empty(options);
        }
    }
}
