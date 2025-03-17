using Microsoft.AspNetCore.Mvc.Testing;

namespace UsersTests.UsersAPI.Controllers.Senders;

public class SenderModuleControllerUnitTestCase : IClassFixture<WebApplicationFactory<Program>>
{
    
    private readonly WebApplicationFactory<Program> _factory;
    public readonly HttpClient _client;

    public SenderModuleControllerUnitTestCase(WebApplicationFactory<Program> factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
    }
}