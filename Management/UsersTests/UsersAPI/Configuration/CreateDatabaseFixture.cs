using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using UsersManagement.Users.Domain;

namespace UsersTests.UsersAPI.Configuration;

public class CreateDatabaseFixture : IAsyncLifetime
{
    
    public MySqlContainer container;
    public async Task InitializeAsync()
    {
        /*string outputPath = @"..\..\..\..\mysql";
        string _filePath = Path.GetFullPath(outputPath);*/
        container = new MySqlBuilder()
            .WithDatabase("testdbdocker")
            .WithUsername("admin")
            .WithPassword("admin")
            //.WithBindMount()
            .Build();
        await container.StartAsync();

        DockerConnectionStringProvider.ConnectionString = container.GetConnectionString();
        
        this.EnsureDatabaseIsCreated();
    }

    public async Task DisposeAsync()
    {
        await container.DisposeAsync();
    }

    private void EnsureDatabaseIsCreated()
    {
        string connectionString = container.GetConnectionString();
        this.AddFluentMigrator(connectionString, Assembly.Load("UsersTests"));
    }

    private void AddFluentMigrator(string connectionString, params Assembly[] assemblies)
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(
                builder => builder
                    .AddMySql5()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(assemblies).For.Migrations())
            .BuildServiceProvider(false);

        using IServiceScope scope = serviceProvider.CreateScope();
        IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
    
}
[CollectionDefinition("Tests collection")]
public class FixtureCollection : ICollectionFixture<CreateDatabaseFixture> {}