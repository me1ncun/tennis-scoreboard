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
            connection.Query<Match>("INSERT INTO [Matches] (Player1, Player2, Winner) VALUES (@p1, @p2, @w);", new { p1 = player1Id, p2 = player2Id, w = winnerId });
        }
    }
    
    public List<Match> GetAll()
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return  connection.Query<Match>("SELECT * FROM [Matches];").ToList();
        }
    }
    
    public Match GetMatchByGuid(Guid id)
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
            return connection.QueryFirstOrDefault<int>("SELECT [ID] FROM [Players] WHERE Name = @n;", new { n = name });
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
            /*string query = @"SELECT * FROM [Matches] WHERE Player1 = @userID OR Player2 = @userID;";*/

            return connection.Query<Match>(query, new { n = name }).ToList();
        }
    }
}