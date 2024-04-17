using Dapper;
using frontend.Repositories;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class PlayerRepository : IPlayerRepository
{
    public void RegisterIfNotExist(string name)
    {
        var playerExist = GetPlayerByName(name);
        if(playerExist != null)
        {
            Console.WriteLine("User already exists");
        }
        else
        {
            using (var connection = AppDbContext.CreateConnection())
            {
                try
                {
                    string query = "INSERT INTO [Players] (Name) VALUES (@n);";
                    connection.Query(query, new { n = name });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
    
    public List<Player> GetAllPlayers()
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = "SELECT * FROM [Players]";
            
            return connection.Query<Player>(query).ToList();
        }
    }
    
    public Player GetPlayerByName(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = "SELECT * FROM [Players] WHERE Name = @n";
            
            return connection.QueryFirstOrDefault<Player>(query, new { n = name });
        }
    }
}