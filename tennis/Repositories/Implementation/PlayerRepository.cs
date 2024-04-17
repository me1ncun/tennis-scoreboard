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
                    connection.Query("INSERT INTO [Players] (Name) VALUES (@n);", new { n = name });
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
            return connection.Query<Player>("SELECT * FROM [Players];").ToList();
        }
    }
    
    public Player GetPlayerByName(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Player>("SELECT * FROM [Players] WHERE Name = @n;", new { n = name });
        }
    }
}