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
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ConfigurationManagementSystem.Api.Tests.Tests
{
    public class EnvironmentsControllerTests : ControllerTests
    {
        [Fact]
        public async void GetAll_NotExists_ReturnsEmptyArray()
        {
            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, "api/environments");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<EnvironmentDto>>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(actual);
        }

        [Fact]
        public async void GetAll_ExistsSingleWithHierarchy_ReturnsSingleWithHierarchy()
        {
            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName("Dev"));
            var root = env.GetRootOptionGroop();
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
            
            var request = new HttpRequestMessage(HttpMethod.Get, "api/environments");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<IEnumerable<EnvironmentDto>>();
            var actualList = actual.ToList();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Single(actualList);
            Assertions.EnvironmentDtosAreEquivalentToModel(actualList, env);
        }

        [Fact]
        public async void GetById_NotExists_Returns404()
        {
            ActWithDbContext(context =>
            {
                new ContextSetup<ConfigurationServiceContext>(context)
                    .Initialize();
            });

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments/{Guid.NewGuid()}");
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void GetById_Exists_ReturnsDto()
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

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments/{env.Id}");
            var response = await HttpClient.SendAsync(request);
            var actual = await response.ParseContentAsync<EnvironmentDto>();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assertions.EnvironmentDtoIsEquivalentToModel(actual, env);
        }

        [Fact]
        public async void Post_ExistsWithTheSameName_Returns422()
        {
            const string envName = "SomeEnv";

            var project = Project.Create(new ProjectName("TestProject"), new ApiKey(Guid.NewGuid()));
            var env = project.AddEnvironment(new EnvironmentName(envName));
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

            var body = new
            {
                project = project.Id.ToString(),
                name = envName
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/environments")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await HttpClient.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async void Post_NotExisted_Returns201AndDto()
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

            var body = new
            {
                project = project.Id.ToString(),
                name = "NewEnv"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/environments")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            var response = await HttpClient.SendAsync(request);
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

            ActWithDbContext(context =>
            {
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
            });
                
            var body = new
            {
                project = project.Id.ToString(),
                name = newEnvName
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/environments")
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            _ = await HttpClient.SendAsync(request);

            ActWithDbContext(context =>
            {
                var proj = context.Projects.Include(x => x.Environments)
                    .AsSingleQuery()
                    .AsNoTrackingWithIdentityResolution()
                    .First(x => x.Id == project.Id);

                Assert.Contains(proj.Environments, x => x.Name.Value == newEnvName);
            });
        }
    }
}
