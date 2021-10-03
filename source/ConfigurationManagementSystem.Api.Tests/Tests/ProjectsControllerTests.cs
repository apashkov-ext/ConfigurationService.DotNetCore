using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions;
using ConfigurationManagementSystem.Api.Tests.Extensions;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Persistence.ContextInitialization;
using Xunit;

namespace ConfigurationManagementSystem.Api.Tests.Tests
{

    public class ProjectsControllerTests : ControllerTests
    {
        [Fact]
        public async void GetAll_NotExists_ReturnsEmptyArray()
        {
            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context).Initialize();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<ProjectDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(actual);
        }

        [Fact]
        public async void GetAll_ExistsSingleWithoutHierarchy_ReturnsSingleWithoutHierarchy()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));

            ActWithDbContext(context => 
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .Save();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<ProjectDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual);
            Assertions.ProjectDtosAreEquivalentToModel(actual, project);
        }

        [Fact]
        public async void GetAll_ExistsSingleWithHierarchy_ReturnsSingleWithHierarchy()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var root = env.OptionGroups.First();
            var group = root.AddNestedGroup(new OptionGroupName("NestedG"), new Description("Desc"));
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .WithEntities(env)
                    .WithEntities(root, group)
                    .WithEntities(option)
                    .Save();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<ProjectDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actual);
            Assertions.ProjectDtosAreEquivalentToModel(actual, project);
        }

        [Fact]
        public async void GetById_NotExists_Returns404()
        {
            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects/{Guid.NewGuid()}");
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void GetById_Exists_ReturnsWithHierarchy()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .WithEntities(env)
                    .WithEntities(group)
                    .WithEntities(option)
                    .Save();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/projects/{project.Id}");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<ProjectDto>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assertions.ProjectDtoIsEquivalentToModel(actual, project);
        }

        [Fact]
        public async void Post_Exists_Returns422()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize()
                .WithEntities(project)
                .Save();
            });

            var body = new
            {
                name = "TestProject"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/projects")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async void Post_NotExists_Returns201AndCorrectResponse()
        {
            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();
            });

            var body = new
            {
                name = "TestProject"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/projects")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<CreatedProjectDto>();

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(body.name, actual.Name);
        }

        [Fact]
        public async void Delete_NotExists_Returns404()
        {
            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                .Initialize();
            });

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{Guid.NewGuid()}");
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Delete_ExistsWithNoHierarchy_ReturnsNoContent()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .Save();
            });
                
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void Delete_ExistsWithFullHierarchy_ReturnsNoContent()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .WithEntities(env)
                    .WithEntities(group)
                    .WithEntities(option)
                    .Save();
            });

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void Delete_Exists_RemovesEntityFromContext()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .Save();
            });

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            _ = await HttpClient.SendAsync(request);

            ActWithDbContext(context =>
            {
                var projects = context.Projects.ToList();
                Assert.Empty(projects);
            });
        }

        [Fact]
        public async void Delete_ExistsWithFullHierarchy_RemovesAllDependentEntities()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));

            var group = env.GetRootOptionGroop();
            var option = group.AddOption(new OptionName("OptionName"), new Description(""), new OptionValue(true));

            var nestedGroup = group.AddNestedGroup(new OptionGroupName("Nested"), new Description("Some nested froup"));
            var nestedOption = nestedGroup.AddOption(new OptionName("SomeOpt"), new Description("Some nested option"), new OptionValue(888));

            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize()
                    .WithEntities(project)
                    .WithEntities(env)
                    .WithEntities(group, nestedGroup)
                    .WithEntities(option, nestedOption)
                    .Save();
            });

            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/projects/{project.Id}");
            _ = await HttpClient.SendAsync(request);

            ActWithDbContext(context =>
            {
                var environments = context.Environments.ToList();
                var groups = context.OptionGroups.ToList();
                var options = context.Options.ToList();

                Assert.Empty(environments);
                Assert.Empty(groups);
                Assert.Empty(options);
            });
        }
    }
}
