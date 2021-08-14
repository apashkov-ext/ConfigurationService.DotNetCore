using ConfigurationService.Tests.Presets;
using Xunit;
using Environment = ConfigurationService.Domain.Entities.Environment;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfigurationService.Persistence;
using System.Linq;
using System.Net;
using System.Text.Json;
using ConfigurationService.Api.Dto;
using AutoFixture;
using FluentAssertions;

namespace ConfigurationService.Api.Tests
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
        public async void Get_EnvironmentById_Success()
        {
            var scopeFactory = _webAppFactory.Services;
            using var scope = scopeFactory.CreateScope();
            
            var context = scope.ServiceProvider.GetService<ConfigurationServiceContext>();
            await TestContextInitializer.InitializeAsync(context);

            var id = context.Environments.First().Id;
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/environments/{id}");

            var response = await _httpClient.SendAsync(request);

            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var expected = fixture.Build<EnvironmentDto>().Create();

            response
                .Should()
                .Be200Ok()
                .And.BeAs(expected);
        }
    }
}
