namespace UsersTests;
[Collection("Tests collection")]
public class ApiTestCase
{
    private readonly TestWebApplicationFactory _factory;
    protected readonly HttpClient HttpClient;

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
}