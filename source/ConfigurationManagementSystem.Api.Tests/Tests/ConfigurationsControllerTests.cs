using System;
using System.Linq;
using System.Net.Http;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions;
using ConfigurationManagementSystem.Api.Tests.Extensions;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ConfigurationManagementSystem.Api.Tests.Tests;

public class ConfigurationsControllerTests : ControllerTests
{
    [Fact]
    public async void GetAll_NotExists_ReturnsEmptyArray()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup().Initialize();
        });

        var actual = await GetAsync<PagedResponseDto<ConfigurationDto>>("api/configurations");

        Assert.Equal(System.Net.HttpStatusCode.OK, actual.StatusCode);
        Assert.Empty(actual.ResponseData.Data);
    }

    [Fact]
    public async void GetAll_ExistsSingleWithHierarchy_ReturnsSingleWithHierarchy()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));
        var env = project.AddConfiguration(new ConfigurationName("Dev"));
        var root = env.GetRootOptionGroop();
        var group = root.AddNestedGroup(new OptionGroupName("NestedG"));
        var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(project)
                .AddEntities(env)
                .AddEntities(root, group)
                .AddEntities(option)
                .Initialize();
        });

        var actual = await GetAsync<PagedResponseDto<ConfigurationDto>>("api/configurations?hierarchy=true");

        Assert.Equal(System.Net.HttpStatusCode.OK, actual.StatusCode);
        Assert.Single(actual.ResponseData.Data);
        Assertions.ConfigurationDtosAreEquivalentToModel(actual.ResponseData.Data, env);
    }

    [Fact]
    public async void GetById_NotExists_Returns404()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup().Initialize();
        });

        var actual = await GetAsync<ConfigurationDto>($"api/configurations/{Guid.NewGuid()}");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, actual.StatusCode);
    }

    [Fact]
    public async void GetByIdWithHierarchy__Exists_ReturnsDto()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));
        var env = project.AddConfiguration(new ConfigurationName("Dev"));
        var group = env.OptionGroups.First();
        var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(project)
                .AddEntities(env)
                .AddEntities(group)
                .AddEntities(option)
                .Initialize();
        });

        var actual = await GetAsync<ConfigurationDto>($"api/configurations/{env.Id}?hierarchy=true");

        Assert.Equal(System.Net.HttpStatusCode.OK, actual.StatusCode);
        Assertions.ConfigurationDtoIsEquivalentToModel(actual.ResponseData, env);
    }

    [Fact]
    public async void Post_ExistsWithTheSameName_Returns422()
    {
        const string configName = "SomeConfig";

        var project = ApplicationEntity.Create(new ApplicationName("TestApp"), new ApiKey(Guid.NewGuid()));
        var env = project.AddConfiguration(new ConfigurationName(configName));
        var group = env.OptionGroups.First();
        var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(project)
                .AddEntities(env)
                .AddEntities(group)
                .AddEntities(option)
                .Initialize();
        });

        var body = new
        {
            application = project.Id.ToString(),
            name = configName
        };

        var actual = await PostAsync<ConfigurationDto>($"api/configurations", body);

        Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, actual.StatusCode);
    }

    [Fact]
    public async void Post_NotExisted_Returns201AndDto()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));
        var env = project.AddConfiguration(new ConfigurationName("Dev"));
        var group = env.OptionGroups.First();
        var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(project)
                .AddEntities(env)
                .AddEntities(group)
                .AddEntities(option)
                .Initialize();
        });

        var body = new
        {
            application = project.Id.ToString(),
            name = "NewEnv"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/configurations")
        {
            Content = RequestContentFactory.CreateJsonStringContent(body)
        };
        var response = await HttpClient.SendAsync(request);
        var actual = await response.ParseContentAsync<ConfigurationDto>();

        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(body.name, actual.Name);
        Assert.True(body.application.Equals(actual.ApplicationId.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async void Post_NotExisted_ProjectContainsNewEnv()
    {
        const string newEnvName = "NewEnv";
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));

        ActWithDbContext(context =>
        {
            var env = project.AddConfiguration(new ConfigurationName("Dev"));
            var group = env.OptionGroups.First();
            var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(project)
                .AddEntities(env)
                .AddEntities(group)
                .AddEntities(option)
                .Initialize();
        });

        var body = new
        {
            application = project.Id.ToString(),
            name = newEnvName
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/configurations")
        {
            Content = RequestContentFactory.CreateJsonStringContent(body)
        };
        _ = await HttpClient.SendAsync(request);

        ActWithDbContext(context =>
        {
            var proj = context.Applications.Include(x => x.Configurations)
                .AsSingleQuery()
                .AsNoTrackingWithIdentityResolution()
                .First(x => x.Id == project.Id);

            Assert.Contains(proj.Configurations, x => x.Name.Value == newEnvName);
        });
    }
}
