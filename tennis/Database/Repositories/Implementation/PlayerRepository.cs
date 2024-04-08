
using Dapper;
using frontend.Repositories;
using tennis_scoreboard.Models;

public class PlayerRepository : IPlayerRepository
{
    public void Register(string name) 
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            connection.Query<Player>("INSERT INTO [Players] (Name) VALUES (@n);", new { n = name });
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