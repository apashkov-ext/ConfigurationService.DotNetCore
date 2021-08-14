using Xunit;
using System.Net.Http;
using System.Net;

namespace ConfigurationService.Api.Tests
{
    public class BasicTests : IClassFixture<WebAppFactory>
    {
        private readonly HttpClient _httpClient;

        public BasicTests(WebAppFactory webAppFactory)
        {
            _httpClient = webAppFactory.CreateClient();
        }

        [Theory]
        [InlineData("projects")]
        [InlineData("environments")]
        [InlineData("options")]
        [InlineData("option-groups")]
        public async void Get_WithoutParams_ReturnsOk(string url)
        {            
            var response = await _httpClient.GetAsync($"api/{url}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("projects")]
        [InlineData("environments")]
        [InlineData("options")]
        [InlineData("option-groups")]
        public async void Get_WithoutParams_ReturnsCorrectContentType(string url)
        {
            var response = await _httpClient.GetAsync($"api/{url}");

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
