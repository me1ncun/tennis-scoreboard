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
    public async void RegisterIfNotExist(string name)
    {
        var playerExist =  await GetPlayerByName(name);
        if (playerExist != null)
        {
            Console.WriteLine("User already exists");
        }
        else
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
            {
                string query = """INSERT INTO players(name) VALUES (@name)""";

                await connection.QueryAsync(query, new { name });
            }
        }
    }

    public async Task<Player> GetPlayerByName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = """SELECT * FROM players WHERE (name) = @name""";

            return await connection.QuerySingleOrDefaultAsync<Player>(query, new { name });
        }
    }
    
    public async Task<string> GetNameById(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = """SELECT (name) FROM players WHERE (id) = @id;""";

            return await connection.QuerySingleOrDefaultAsync<string>(query, new { id });
        }
    }
    
    public async Task<int> GetIdByName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = """SELECT (id) FROM players WHERE (name) = @name;""";

            return await connection.QuerySingleOrDefaultAsync<int>(query, new { name });
        }
    }
    
}