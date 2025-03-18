using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Users.Shared.HttpClient;
using UsersTests.UsersAPI.Configuration;

namespace UsersTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<IHttpClientService> HttpClientServiceMock { get; private set; }
    
    public TestWebApplicationFactory()
    {
        HttpClientServiceMock = new Mock<IHttpClientService>();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureServices(services =>
            {
                // Add the mock HTTP client service
                services.AddScoped<IHttpClientService>(sp => HttpClientServiceMock.Object);
                services.RemoveDatabaseConfig();
                services.AddMySqlDockerConfig();
            });
    }
    
}