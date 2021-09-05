using Xunit;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationService.Persistence;
using ConfigurationService.Api.Dto;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using System;
using System.Text.Json;
using ConfigurationService.Application;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain;
using ConfigurationService.Persistence.ContextInitialization;
using System.Text;
using ConfigurationService.Api.Tests.DtoAssertions;

namespace ConfigurationService.Api.Tests
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
                .Initialize()
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await _httpClient.SendAsync(request);
            var actual = await ParseContentAsync<IEnumerable<ProjectDto>>(response);

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
            var actual = await ParseContentAsync<IEnumerable<ProjectDto>>(response);

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
            var actual = await ParseContentAsync<IEnumerable<ProjectDto>>(response);

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
                .Initialize()
                .Save();

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
            var actual = await ParseContentAsync<ProjectDto>(response);

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
                Content = ToJsonContent(body)
            };
            var response = await _httpClient.SendAsync(request);
            var actual = await ParseContentAsync<CreatedProjectDto>(response);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        private static async Task<T> ParseContentAsync<T>(HttpResponseMessage response) 
        {
            var json = await response.Content.ReadAsStringAsync();
            var actual = JsonSerializer.Deserialize<T>(json, SerializerOptions.JsonSerializerOptions);
            return actual;
        }

        private static StringContent ToJsonContent(object data)
        {
            var json = JsonSerializer.Serialize(data, SerializerOptions.JsonSerializerOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
