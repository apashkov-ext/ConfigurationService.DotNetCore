using System;
using System.Linq;
using System.Net.Http;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Tests.DtoAssertions;
using ConfigurationManagementSystem.Domain;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;
using ConfigurationManagementSystem.Persistence;
using ConfigurationManagementSystem.Tests.Fixtures.ContextInitialization;
using Xunit;

namespace ConfigurationManagementSystem.Api.Tests.Tests;

public class ApplicationsControllerTests : ControllerTests
{
    [Fact]
    public async void GetAll_Empty_ReturnsCorrectType()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context).Setup().Initialize();
        });

        var actual = await GetAsync<PagedResponseDto<ApplicationDto>>("api/applications");

        Assert.Equal(System.Net.HttpStatusCode.OK, actual.StatusCode);
        Assert.Empty(actual.ResponseData.Data);
    }

    [Fact]
    public async void GetAll_NotExists_ReturnsEmptyResponse()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context).Setup().Initialize();
        });

        var response = await GetAsync<PagedResponseDto<ApplicationDto>>("api/applications");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Empty(response.ResponseData.Data);
    }

    [Fact]
    public async void GetAll_ExistsSingleWithoutHierarchy_ReturnsSingleWithoutHierarchy()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(project)
                .Initialize();
        });

        var response = await GetAsync<PagedResponseDto<ApplicationDto>>("api/applications");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Single(response.ResponseData.Data);
        Assertions.ApplicationDtosAreEquivalentToModel(response.ResponseData.Data, project);
    }

    [Fact]
    public async void GetAll_ExistsSingleWithHierarchy_ReturnsSingleWithHierarchy()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));
        var env = project.AddConfiguration(new ConfigurationName("Dev"));
        var root = env.OptionGroups.First();
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

        var response = await GetAsync<PagedResponseDto<ApplicationDto>>("api/applications?hierarchy=true");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Single(response.ResponseData.Data);
        Assertions.ApplicationDtosAreEquivalentToModel(response.ResponseData.Data, project);
    }

    [Fact]
    public async void GetById_NotExists_Returns404()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context).Setup().Initialize();
        });

        var response = await GetAsync($"api/applications/{Guid.NewGuid()}");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async void GetById_Exists_ReturnsWithHierarchy()
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

        var response = await GetAsync<ApplicationDto>($"api/applications/{project.Id}?hierarchy=true");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assertions.ApplicationDtoIsEquivalentToModel(response.ResponseData, project);
    }

    [Fact]
    public async void Post_Exists_Returns422()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
            .Setup()
            .AddEntities(project)
            .Initialize();
        });

        var body = new
        {
            name = "TestProject"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "api/applications")
        {
            Content = RequestContentFactory.CreateJsonStringContent(body)
        };
        var response = await PostAsync("api/applications", body);

        Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async void Post_NotExists_Returns201AndCorrectResponse()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
            .Setup().Initialize();
        });

        var body = new
        {
            name = "TestProject"
        };

        var response = await PostAsync<CreatedApplicationDto>("api/applications", body);

        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(body.name, response.ResponseData.Name);
    }

    [Fact]
    public async void Delete_NotExists_Returns404()
    {
        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
            .Setup().Initialize();
        });

        var response = await DeleteAsync($"api/applications/{Guid.NewGuid()}");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async void Delete_ExistsWithNoHierarchy_ReturnsNoContent()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));

        await ActWithDbContextAsync(init =>
        {
            init.AddEntities(project).Initialize();
        }, async ctx =>
        {
            var response = await DeleteAsync($"api/applications/{project.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        });
    }

    [Fact]
    public async void Delete_ExistsWithFullHierarchy_ReturnsNoContent()
    {
        var project = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));
        var env = project.AddConfiguration(new ConfigurationName("Dev"));
        var group = env.OptionGroups.First();
        var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));

        await ActWithDbContextAsync(init =>
        {
            init.AddEntities(project)
                .AddEntities(env)
                .AddEntities(group)
                .AddEntities(option)
                .Initialize();
        }, async ctx =>
        {
            var response = await DeleteAsync($"api/applications/{project.Id}");

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        });
    }

    [Fact]
    public async void Delete_Exists_RemovesEntityFromContext()
    {
        var app = ApplicationEntity.Create(new ApplicationName("TestApp"), new ApiKey(Guid.NewGuid()));

        await ActWithDbContextAsync(init =>
        {
            init.AddEntities(app).Initialize();
        }, async context =>
        {
            await DeleteAsync($"api/applications/{app.Id}");

            var projects = context.Applications.ToList();
            Assert.Empty(projects);
        });
    }

    [Fact]
    public async void Delete_ExistsWithFullHierarchy_RemovesAllDependentEntities()
    {
        var app = ApplicationEntity.Create(new ApplicationName("TestProject"), new ApiKey(Guid.NewGuid()));
        var config = app.AddConfiguration(new ConfigurationName("Dev"));

        var group = config.GetRootOptionGroop();
        var option = group.AddOption(new OptionName("OptionName"), new OptionValue(true));

        var nestedGroup = group.AddNestedGroup(new OptionGroupName("Nested"));
        var nestedOption = nestedGroup.AddOption(new OptionName("SomeOpt"), new OptionValue(888));

        ActWithDbContext(context =>
        {
            new ContextSetup<ConfigurationManagementSystemContext>(context)
                .Setup()
                .AddEntities(app)
                .AddEntities(config)
                .AddEntities(group, nestedGroup)
                .AddEntities(option, nestedOption)
                .Initialize();
        });

        await DeleteAsync($"api/applications/{app.Id}");

        ActWithDbContext(context =>
        {
            var applications = context.Applications.ToList();
            var environments = context.Configurations.ToList();
            var groups = context.OptionGroups.ToList();
            var options = context.Options.ToList();

            Assert.Empty(applications);
            Assert.Empty(environments);
            Assert.Empty(groups);
            Assert.Empty(options);
        });
    }
}
