using System.Runtime.Intrinsics.X86;
using Dapper;
using frontend.Repositories;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class MatchesRepository: IMatchesRepository
{
    public void Create(int player1Id, int player2Id, int winnerId)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = @"INSERT INTO [Matches] (Player1, Player2, Winner)
                                         VALUES (@p1, @p2, @w)";
            
            connection.Query<Match>(query, new { p1 = player1Id, p2 = player2Id, w = winnerId });
        }
    }
    
    public List<Match> GetAll()
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = "SELECT * FROM [Matches]";
            
            return  connection.Query<Match>(query).ToList();
        }
    }
    
    public Match GetMatchByGuid(Guid id)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = "SELECT * FROM [Matches] WHERE Id = @i";
            
            return connection.QueryFirstOrDefault<Match>(query, new { i = id });
        }
    }
    
    public string GetNameById(int id)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = "SELECT Name FROM [Players] WHERE ID = @i";
            
            return connection.QueryFirstOrDefault<string>(query, new { i = id });
        }
    }
    
    public int GetIdByName(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = "SELECT [ID] FROM [Players] WHERE Name = @n";
            
            return connection.QueryFirstOrDefault<int>(query, new { n = name });
        }
    }

    public List<Match> GetMatchesByPlayerName(string name)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            string query = @"SELECT * 
                         FROM Matches m 
                         INNER JOIN Players p ON m.Player1 = p.ID OR m.Player2 = p.ID
                         WHERE p.Name = @n";
            
            return connection.Query<Match>(query, new { n = name }).ToList();
        }
    }
}