using Moq;
using Users.Shared.HttpClient;

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

    protected void ShouldFindVehicleByHttp(HttpResponseMessage message)
    {
        HttpClientService.Setup(h => h.GetAsync(It.IsAny<string>())).ReturnsAsync(message);
    }
    
}