using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using UsersManagement.Vehicles.Domain;
using UsersManagement.Vehicles.Domain.ValueObject;

namespace UsersManagement.Vehicles.Infrastructure;

public class VehicleRepository : IVehicleRepository
{
    
    private readonly IDbConnection _connection;

    public VehicleRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public void Save(Vehicle vehicle)
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            string query = "INSERT INTO vehicle (id, registration, color) VALUES (@id, @registration, @color)";
            connection.Execute(query,
                new { id = vehicle.Id, registration = vehicle.VehicleRegistration, color = vehicle.VehicleColor.ToString() });
        }
        
        
    }

    public Vehicle GetById(VehicleId id)
    {

        using var connection = new MySqlConnection(_connection.ConnectionString);
        {
            string query =
                "SELECT id as Id, registration as VehicleRegistration, color as VehicleColor FROM vehicle WHERE id = @Id";
            var vehicle = connection.QueryFirstOrDefault<Vehicle>(query, new { Id = id.IdValue });
            return vehicle;
        }
        
        
        
    }

    public async Task<IEnumerable<Vehicle>> Search()
    {
        using (var connection = new MySqlConnection(_connection.ConnectionString))
        {
            string query = "SELECT id as Id, color as VehicleColor, registration as VehicleRegistration FROM vehicle";
            return await connection.QueryAsync<Vehicle>(query);
        }
        
        
        
    }
}