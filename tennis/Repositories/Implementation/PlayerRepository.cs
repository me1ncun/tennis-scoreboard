using Dapper;
using frontend.Repositories;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class PlayerRepository : IPlayerRepository
{
    public void Register(string name) 
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            try
            {
                connection.Query("INSERT INTO [Players] (Name) VALUES (@n);", new { n = name });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
    public bool Exists(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Player>("SELECT * FROM [Players] WHERE Name = @n;", new { n = name }) != null;
        }
    }
    
    public List<Player> GetAll()
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.Query<Player>("SELECT * FROM [Players];").ToList();
        }
    }
    
    public Player GetByName(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Player>("SELECT * FROM [Players] WHERE Name = @n;", new { n = name });
        }
    }
}