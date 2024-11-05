using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Common.Services;
public class HttpService
{
    private readonly ILogger<HttpService> _logger;
    public HttpService(ILogger<HttpService> logger)
    {
        _logger = logger;
    }

    private async Task<T> SendAsync<T>(HttpMethod method, string service, string requestUri, string? payload)
        where T : class
    {
        using var client = new HttpClient();
        var serviceUri = new Uri($"http://{service}:8080/api/");
        client.BaseAddress = serviceUri;

        var request = new HttpRequestMessage(method, requestUri);
        if (payload != null)
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null || content == String.Empty)
                    return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return default;
            }
            _logger.LogError($"Recieved from {service}: {response.StatusCode}");
            _logger.LogError($"Attached message:\n{response.ReasonPhrase}");
            return default;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex.Message);
            return default;
        }
    }

    public async Task<T> GetFromServiceAsync<T>(string service, string requestUri)
        where T : class
    {
        var result = await SendAsync<T>(HttpMethod.Get, service, requestUri, null);
        return result;
    }

    public async Task<T> PostToServiceAsync<T>(string service, string requestUri, string payload)
        where T : class
    {
        var result = await SendAsync<T>(HttpMethod.Post, service, requestUri, payload);
        return result;
    }
    public async Task<T> PutToServiceAsync<T>(string service, string requestUri, string payload)
        where T : class
    {
        var result = await SendAsync<T>(HttpMethod.Put, service, requestUri, payload);
        return result;
    }

    public async Task<T> DeleteFromServiceAsync<T>(string service, string requestUri)
        where T : class
    {
        var result = await SendAsync<T>(HttpMethod.Delete, service, requestUri, null);
        return result;
    }
}