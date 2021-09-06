using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Tests.DtoAssertions;
using ConfigurationService.Api.Tests.Extensions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence;
using ConfigurationService.Persistence.ContextInitialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConfigurationService.Api.Tests.Tests
{
    public class EnvironmentsControllerTests : IClassFixture<WebAppFactory>
    {
        private readonly WebAppFactory _webAppFactory;
        private readonly HttpClient _httpClient;

        public EnvironmentsControllerTests(WebAppFactory webAppFactory)
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

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<EnvironmentDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(actual);
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

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<EnvironmentDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual);
            Assertions.EnvironmentDtosAreEquivalentToModel(actual, env);
        }

        [Fact]
        public async void GetById_NotExists_Returns404()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments");
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void GetById_Exists_ReturnsDto()
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

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments");
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<EnvironmentDto>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assertions.EnvironmentDtoIsEquivalentToModel(actual, env);
        }

        [Fact]
        public async void Post_ExistsWithTheSameName_Returns422()
        {
            const string envName = "SomeEnv";

            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName(envName));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));
            new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .WithEntities(env)
                .WithEntities(group)
                .WithEntities(option)
                .Save();

            var body = new
            {
                project = project.Id.ToString(),
                name = envName
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/environments")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await _httpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async void Post_NotExisted_Returns201AndDto()
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

            var body = new
            {
                project = project.Id.ToString(),
                name = "NewEnv"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/environments")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await _httpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<EnvironmentDto>();

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(body.name, actual.Name);
            Assert.True(body.project.Equals(actual.ProjectId.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async void Post_NotExisted_ProjectContainsNewEnv()
        {
            const string newEnvName = "NewEnv";
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));

            var scopeFactory = _webAppFactory.Services;
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
                
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
            }
                
            var body = new
            {
                project = project.Id.ToString(),
                name = newEnvName
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/environments")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await _httpClient.SendAsync(request);

            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
                var proj = context.Projects.Include(x => x.Environments)
                    .AsSingleQuery()
                    .AsNoTrackingWithIdentityResolution()
                    .First(x => x.Id == project.Id);

                Assert.Contains(proj.Environments, x => x.Name.Value == newEnvName);
            }
        }
    }
}
