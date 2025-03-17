using System.Data;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UsersManagement.Users.Domain;
using UsersTests.UsersAPI.Configuration;

namespace UsersTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

    builder.ConfigureServices(services =>
        {
            services.RemoveDatabaseConfig();
            services.AddMySqlDockerConfig();
        });
    }
    
}