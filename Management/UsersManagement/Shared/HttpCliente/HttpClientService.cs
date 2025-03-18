using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace UsersManagement.Shared.HttpClient;

public class HttpClientService : IHttpClientService
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public HttpClientService(System.Net.Http.HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        return await _httpClient.GetAsync(url);
    }
    

    public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T data)
    {
        return await _httpClient.PostAsJsonAsync(url, data);
    }
}