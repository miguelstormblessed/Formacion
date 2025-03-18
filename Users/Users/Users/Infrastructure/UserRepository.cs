using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Users.Shared.Vehicles.Domain.Responses;
using UsersManagement.Users.Domain;
using UsersManagement.Users.Domain.ValueObject;

namespace UsersManagement.Users.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        
        
        
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            this._connection = connection;
        }

        public async Task<IEnumerable<Usuario>> SearchUsers()
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                const string sql = "SELECT id, email, name, state  FROM user";
                const string vehicleQuery = "SELECT vehicles  FROM user";
                var usersData = await connection.QueryAsync<Usuario>(sql, new { });
                var jsonVehicles = await connection.QueryAsync<string>(vehicleQuery);

                for (int i = 0; i < usersData.Count(); i++)
                {
                    if (jsonVehicles.ElementAt(i) is string jsonString && !string.IsNullOrEmpty(jsonString))
                    {
                        usersData.ElementAt(i).Vehicle = VehicleResponse.FromJson(jsonString);
                    }
                    else
                    {
                        usersData.ElementAt(i).Vehicle = VehicleResponse.Create("Vehicle not found", "Vehicle not found", "Vehicle not found");
                    }
                }
                return usersData;
            }
        }

        public void Save(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                string json = usuario.Vehicle.ToJson();
                var sql = "INSERT INTO user (id,email, name, state,vehicles) VALUES (@Id, @Email, @Name, @Active, @json)";
                connection.Execute(sql, new{Id = usuario.Id.Id, Email = usuario.Email.Email, Name = usuario.Name.Name, Active = usuario.State.Active, json = json});
            }
        }

        public void Update(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                string? vehicleJson = null;
                if (usuario.Vehicle != null)
                {
                    vehicleJson = usuario.Vehicle.ToJson();
                }
                
                var sql = "UPDATE user SET email=@Email, name = @Name, state = @Active, vehicles=@json WHERE id = @Id";
                connection.Execute(sql, new { usuario.Email.Email, usuario.Name.Name, usuario.Id.Id, usuario.State.Active, json = vehicleJson });   
            }
            
            
        }

        public Usuario? Find(UserId userId)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                var sql = "SELECT id, email, name, state FROM user WHERE id = @Id";
                string jsonQuery = "SELECT vehicles FROM user WHERE id = @Id";
                var result = connection.QueryFirstOrDefault<Usuario>(sql, new { Id = userId.Id });
                string jsonVehicles = connection.QueryFirstOrDefault<string>(jsonQuery, new { Id = userId.Id });
                if (jsonVehicles != null)
                {
                    result.Vehicle = VehicleResponse.FromJson(jsonVehicles);
                }
                
                return result;
            }
        }

        public void Delete(UserId userId)
        {
            using (var connection = new MySqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE user SET state = 0 WHERE id = @Id";
                connection.Execute(sql, new { Id = userId.Id });
            }
        }
    }
}
