using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationService.Application.Exceptions;

namespace ConfigurationService.Application
{
    public class DefaultHttpClient
    {
        private readonly HttpClient _client;

        public DefaultHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var resp = await ExecuteHttpMethod(() => _client.GetAsync(uri));
            return JsonSerializer.Deserialize<T>(await resp.Content.ReadAsStringAsync(), SerializerOptions.JsonSerializerOptions);
        }

        public async Task<T> PostAsync<T>(string uri, object data)
        {
            var content = ToJsonContent(data);
            var resp = await ExecuteHttpMethod(() => _client.PostAsync(uri, content));
            if (resp.Content == null)
            {
                return default;
            }

            return await FromJsonContent<T>(resp.Content);
        }

        public async Task<T> PutAsync<T>(string uri, object data)
        {
            var content = ToJsonContent(data);
            var resp = await ExecuteHttpMethod(() => _client.PutAsync(uri, content));
            if (resp.Content == null)
            {
                return default;
            }

            return await FromJsonContent<T>(resp.Content);
        }

        public Task DeleteAsync(string uri)
        {
            return ExecuteHttpMethod(() => _client.DeleteAsync(uri));
        }

        public Task DeleteAsync(string uri, object data)
        {
            var request = new HttpRequestMessage
            {
                Content = ToJsonContent(data),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri)
            };
            return ExecuteHttpMethod(() => _client.SendAsync(request));
        }

        private static StringContent ToJsonContent(object data)
        {
            var json = JsonSerializer.Serialize(data, SerializerOptions.JsonSerializerOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static async Task<T> FromJsonContent<T>(HttpContent content)
        {
            var jsonResponse = await content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonResponse, SerializerOptions.JsonSerializerOptions);
        }

        private static async Task<HttpResponseMessage> ExecuteHttpMethod(Func<Task<HttpResponseMessage>> method)
        {
            var response = await method();

            try
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException)
            {
                var content = await TryGetContent(response);
                var message = $"An error occurred while accessing the source{(string.IsNullOrEmpty(content) ? string.Empty : $": {content}")}";

                throw (int)response.StatusCode switch
                {
                    404 => new NotFoundException(message),
                    _ => new InternalException(message)
                };
            }
        }

        private static async Task<string> TryGetContent(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return null;
            }


            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                return await response.Content.ReadAsStringAsync();
            }

            var parsed = await FromJsonContent<ErrorObject>(response.Content);
            return parsed.Message;
        }

        private class ErrorObject
        {
            public string Message { get; set; }
        }
    }
}