using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class HttpService : IHttpService
{
    public HttpClient Client { get; }
    private readonly ILogger _logger;

    public HttpService(HttpClient client, IHttpContextAccessor httpContextAccessor, ILogger<HttpService> logger)
    {
        _logger = logger;

        string bearerToken = httpContextAccessor.HttpContext == null ? null : httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

        if (bearerToken != null) client.DefaultRequestHeaders.Add("Authorization", bearerToken);

        Client = client;
    }

    public async Task<T> GetAsync<T>(string requestUrl)
    {
        HttpResponseMessage response = await Client.GetAsync(requestUrl);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsAsync<T>();
    }

    public async Task<byte[]> GetAsByteArrayAsync(string requestUrl)
    {
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await Client.GetAsync(requestUrl);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }

    public async Task<T> PostAsync<T>(string requestUrl, object bodyContent)
    {
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        StringContent content = new StringContent(this.ConvertToContent(bodyContent), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PostAsync(requestUrl, content);

        response.EnsureSuccessStatusCode();

        if (typeof(T) == typeof(String))
        {
            string result = await response.Content.ReadAsStringAsync();

            return (T)Convert.ChangeType(result, typeof(T));
        }
        else if (response.Content.Headers.ContentType.MediaType.Equals("text/html"))
        {
            string result = await response.Content.ReadAsStringAsync();

            return (T)Convert.ChangeType(result, typeof(String));
        }
        else
        {
            T result = await response.Content.ReadAsAsync<T>();

            return result;
        }
    }

    public async Task<byte[]> PostAsByteArrayAsync(string requestUrl, object bodyContent)
    {
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        StringContent content = new StringContent(this.ConvertToContent(bodyContent), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await Client.PostAsync(requestUrl, content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }

    public async Task<string> GetAsyncReturnString(string requestUrl)
    {
        HttpResponseMessage response = await Client.GetAsync(requestUrl);

        return response.StatusCode.ToString();
    }


    private string ConvertToContent(object content) => JsonConvert.SerializeObject(content);

    public async Task<dynamic> PostAsAsync(string requestUrl, object bodyContent)
    {
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        StringContent content = new StringContent(this.ConvertToContent(bodyContent), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PostAsync(requestUrl, content);

        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<T> PostAsync<T>(string requestUrl, HttpContent content)
    {
        HttpResponseMessage response = await Client.PostAsync(requestUrl, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsAsync<T>();
    }

    public async Task PostAsync(string requestUrl, object bodyContent, string tokenType = null, string accessToken = null, string propertyKey = null)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(requestUrl),
            Headers = { { HttpRequestHeader.Accept.ToString(), "application/json" } },
            Content = new StringContent(this.ConvertToContent(bodyContent), Encoding.UTF8, "application/json")
        };

        if (!string.IsNullOrEmpty(accessToken)) httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue($"{tokenType ?? "Bearer"}", accessToken);

        if (!string.IsNullOrEmpty(propertyKey)) httpRequestMessage.Headers.Add("propertyKey", propertyKey);

        try
        {
            HttpResponseMessage response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    public async Task<HttpResponseMessage> PostAsAsync(string requestUrl, object bodyContent, string tokenType, string accessToken, string propertyKey = null)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(requestUrl),
            Headers = { { HttpRequestHeader.Accept.ToString(), "application/json" } },
            Content = new StringContent(this.ConvertToContent(bodyContent), Encoding.UTF8, "application/json")
        };

        if (!string.IsNullOrEmpty(accessToken)) httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue($"{tokenType ?? "Bearer"}", accessToken);
        if (!string.IsNullOrEmpty(propertyKey)) httpRequestMessage.Headers.Add("propertyKey", propertyKey);

        return await Client.SendAsync(httpRequestMessage);
    }
}
