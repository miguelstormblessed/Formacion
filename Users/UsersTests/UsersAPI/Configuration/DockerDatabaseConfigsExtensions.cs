using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace UsersTests.UsersAPI.Configuration;

public static class DockerDatabaseConfigsExtensions
{
    internal static void RemoveDatabaseConfig(this IServiceCollection services)
    {
        ServiceDescriptor? descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnection));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    internal static void AddMySqlDockerConfig(this IServiceCollection services)
    {
        if (string.IsNullOrEmpty(DockerConnectionStringProvider.ConnectionString))
        {
            throw new InvalidOperationException("Docker connection string cannot be null or empty.");
        }
        services.AddTransient<IDbConnection>(_ => new MySqlConnection(DockerConnectionStringProvider.ConnectionString));
    }
}