using Dapper;
using frontend.Repositories;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class MatchesRepository: IMatchesRepository
{
    public void Create(PlayerDTO player1, PlayerDTO player2, PlayerDTO winner)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            connection.Query<Match>("INSERT INTO [Matches] (Player1, Player2, Winner) VALUES (@p1, @p2, @w);", new { p1 = GetIdByName(player1.Name), p2 = GetIdByName(player2.Name), w = GetIdByName(winner.Name) });
        }
    }
    
    public IEnumerable<Match> GetAll()
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.Query<Match>("SELECT * FROM [Matches];").AsEnumerable();
        }
    }
    
    public Match GetById(Guid id)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Match>("SELECT * FROM [Matches] WHERE Id = @i;", new { i = id });
        }
    }
    
    public string GetNameById(int id)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<string>("SELECT Name FROM [Players] WHERE ID = @i;", new { i = id });
        }
    }
    
    public int GetIdByName(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<int>("SELECT ID FROM [Players] WHERE Name = @n;", new { n = name });
        }
    }
}