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
            using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
            {
                await sqlCon.OpenAsync();
                string cmdString = $"INSERT INTO players (name) VALUES (@name)";
        
                using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
                {
                    sqlCmd.Parameters.AddWithValue("name", name);

                    await sqlCmd.ExecuteNonQueryAsync();
                } 
            }
        }
    }

    public async Task<Player> GetPlayerByName(string name)
    {
        using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
        {
            await sqlCon.OpenAsync();
            string cmdString = $"SELECT * FROM players WHERE (name) = @name";
        
            using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
            {
                sqlCmd.Parameters.AddWithValue("name", name);
                
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Player
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            } 
        }
    }
    
    public async Task<string> GetNameById(int id)
    {
        using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
        {
            await sqlCon.OpenAsync();
            string cmdString = $"SELECT (name) FROM players WHERE (id) = @id";
        
            using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
            {
                sqlCmd.Parameters.AddWithValue("id", id);
                
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new string(reader.GetString(reader.GetOrdinal("name")));
                    }
                    else
                    {
                        return null;
                    }
                }
            } 
        }
    }
    
    public async Task<int> GetIdByName(string name)
    {
        using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
        {
            await sqlCon.OpenAsync();
            string cmdString = $"SELECT (id) FROM players WHERE (name) = @n";
        
            using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
            {
                sqlCmd.Parameters.AddWithValue("n", name);
                
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return reader.GetInt32(reader.GetOrdinal("id"));
                    }
                    else
                    {
                        return -1;
                    }
                }
            } 
        }
    }
    
}