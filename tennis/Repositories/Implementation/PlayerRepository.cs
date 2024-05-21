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
    public void RegisterIfNotExist(string name)
    {
        var playerExist =  GetPlayerByName(name);
        if (playerExist != null)
        {
            Console.WriteLine("User already exists");
        }
        else
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
            {
                string query = "INSERT INTO players(name) VALUES (@n);";

                connection.Query(query, new { n = name});
            }
        }
    }

    public IEnumerable<Player> GetPlayerByName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = "SELECT * FROM players WHERE (name) = @name;";

            return connection.Query<Player>(query, new { n = name});
        }
    }
    
    public IEnumerable<string> GetNameById(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = "SELECT (name) FROM players WHERE (id) = @id;";

            return connection.Query<string>(query, new { id = id});
        }
    }
    
    public IEnumerable<int> GetIdByName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = "SELECT (id) FROM players WHERE (name) = @n;";

            return connection.Query<int>(query, new { n = name});
        }
    }
    
}