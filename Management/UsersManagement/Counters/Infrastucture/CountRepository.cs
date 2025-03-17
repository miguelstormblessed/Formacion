using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using UsersManagement.Counters.Domain;

namespace UsersManagement.Counters.Infrastucture;

public class CountRepository : ICountRepository
{
    private readonly IDbConnection _connection;

    public CountRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task Update(Count count)
    {
        using var connection = new MySqlConnection(_connection.ConnectionString);
        {
            connection.Open();
            string query = "UPDATE count SET activeuser = @ActiveUsers, inactiveuser = @InactiveUsers WHERE id = @IdValue";
            await connection.ExecuteAsync(query, new { ActiveUsers = count.ActiveUsers, InactiveUsers = count.InactiveUsers, IdValue = count.Id.IdValue });
        }
        
    }


    public async Task< Count?> Find(string id)
    {
        using var connection = new MySqlConnection(_connection.ConnectionString);
        {
            
            string query = "SELECT id as Id, activeuser AS ActiveUsers, inactiveuser AS InactiveUsers FROM count WHERE id=@id";
            Count count = await connection.QueryFirstOrDefaultAsync<Count>(query, new{id});
            return count;
        }
        

    }
}