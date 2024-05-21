using Dapper;
using frontend.Repositories;
using Npgsql;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class PlayerRepository : IPlayerRepository
{
    private readonly IConfiguration _configuration;
    private readonly string sqlString;
    public PlayerRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        sqlString = _configuration.GetConnectionString("Database");
    }
    public async Task RegisterIfNotExist(string name)
    {
        var playerExist = await GetPlayerByName(name);
        if (playerExist != null)
        {
            Console.WriteLine("User already exists");
        }
        else
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
            {
                string query = $"INSERT INTO players(name) VALUES (@n);";

                connection.Query(query, new { n = name});
            }
        }
    }

    public async Task<IEnumerable<Player>> GetPlayerByName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = $"SELECT * FROM players WHERE name = @n;";

            return await connection.QueryAsync<Player>(query, new { n = name });
        }
    }
    
    public async Task<IEnumerable<string>> GetNameById(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = $"SELECT (name) FROM players WHERE (id) = @id;";

            return await connection.QueryAsync<string>(query, new { id = id});
        }
    }
    
    public async Task<IEnumerable<int>> GetIdByName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = $"SELECT (id) FROM players WHERE (name) = @n;";

            return await connection.QueryAsync<int>(query, new { n = name});
        }
    }
    
}