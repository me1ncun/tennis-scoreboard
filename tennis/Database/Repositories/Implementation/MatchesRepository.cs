using Dapper;
using frontend.Repositories;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class MatchesRepository: IMatchesRepository
{
    public void Create(Player player1, Player player2, Player winner)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            connection.Query<Match>("INSERT INTO [Matches] (Player1, Player2, Winner) VALUES (@p1, @p2, @w);", new { p1 = player1.Id, p2 = player2.Id, w = winner.Id });
        }
    }
    
    public List<Match> GetAll()
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.Query<Match>("SELECT * FROM [Matches];").ToList();
        }
    }
    
    public Match GetById(Guid id)
    {
        using (var connection = AppDbContext.CreateConnection())
        {
            return connection.QueryFirstOrDefault<Match>("SELECT * FROM [Matches] WHERE Id = @i;", new { i = id });
        }
    }
}