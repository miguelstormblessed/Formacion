using Moq;
using UsersManagement.Shared.HttpClient;
using UsersManagement.Shared.Users.Domain.Responses;

namespace UsersTests;
[Collection("Tests collection")]
public class ApiTestCase
{
    private readonly TestWebApplicationFactory _factory;
    protected readonly HttpClient HttpClient;
    protected Mock<IHttpClientService> HttpClientService => _factory.HttpClientServiceMock;
    protected ApiTestCase()
    {
        _factory = new TestWebApplicationFactory();
        HttpClient = _factory.CreateClient();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            HttpClient.Dispose();
            _factory.Dispose();
        }
    }
    protected async Task ShouldFindUserByHttp(HttpResponseMessage response)
    {
        HttpClientService.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
    }
}