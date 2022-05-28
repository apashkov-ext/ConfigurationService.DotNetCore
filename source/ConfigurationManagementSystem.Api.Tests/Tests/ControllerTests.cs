using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Tests;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Api.Tests.Tests
{
    public abstract class ControllerTests : IntegrationTests
    {
        protected readonly HttpClient HttpClient;

        public ControllerTests()
        {
            HttpClient = WebAppFactory.CreateClient();
        }

        protected Task<ControllerResponse> GetAsync(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            return SendAsync(request);
        }

        protected Task<ControllerResponse<T>> GetAsync<T>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            return SendAsync<T>(request);
        }

        protected Task<ControllerResponse> PostAsync(string endpoint, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            return SendAsync(request);
        }

        protected Task<ControllerResponse<T>> PostAsync<T>(string endpoint, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = RequestContentFactory.CreateJsonStringContent(body)
            };
            return SendAsync<T>(request);
        }

        protected Task<ControllerResponse> DeleteAsync(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);
            return SendAsync(request);
        }

        public override void Dispose()
        {
            HttpClient.Dispose();
            base.Dispose();
        }

        private async Task<ControllerResponse> SendAsync(HttpRequestMessage request)
        {
            var response = await HttpClient.SendAsync(request);
            return new ControllerResponse(response.StatusCode);
        }

        private async Task<ControllerResponse<T>> SendAsync<T>(HttpRequestMessage request)
        {
            var response = await HttpClient.SendAsync(request);
            var parsed = await ParseContentAsync<T>(response);
            return new ControllerResponse<T>(parsed, response.StatusCode);
        }

        private static async Task<T> ParseContentAsync<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<T>(json, SerializerOptions.JsonSerializerOptions);
            return dto;
        }
    }

    public class ControllerResponse
    {
        public HttpStatusCode StatusCode { get; }

        public ControllerResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }

    public class ControllerResponse<T> : ControllerResponse
    {
        public T ResponseData { get; }

        public ControllerResponse(T data, HttpStatusCode statusCode) : base(statusCode)
        {
            ResponseData = data;
        }
    }
}
