using Xunit;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationService.Persistence;
using ConfigurationService.Api.Dto;
using FluentAssertions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using System;
using System.Text.Json;
using ConfigurationService.Application;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ConfigurationService.Domain;
using ConfigurationService.Api.Tests.Extensions;

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
                .WithEntities(x => x.Projects, project)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await _httpClient.SendAsync(request);
            var actual = await ParseContentAsync<IEnumerable<ProjectDto>>(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual, x => project.IsEqualToDto(x));
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
                .WithEntities(x => x.Projects, project)
                .WithEntities(x => x.Environments, env)
                .WithEntities(x => x.OptionGroups, group)
                .WithEntities(x => x.Options, option)
                .Save();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await _httpClient.SendAsync(request);
            var actual = await ParseContentAsync<IEnumerable<ProjectDto>>(response);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual, x => project.IsEqualToDto(x));
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

        private static async Task<T> ParseContentAsync<T>(HttpResponseMessage response) 
        {
            var json = await response.Content.ReadAsStringAsync();
            var actual = JsonSerializer.Deserialize<T>(json, SerializerOptions.JsonSerializerOptions);
            return actual;
        }
    }
}
