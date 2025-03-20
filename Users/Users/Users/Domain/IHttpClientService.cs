namespace Users.Users.Domain;

public interface IHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string url);
    
    Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T data);
}