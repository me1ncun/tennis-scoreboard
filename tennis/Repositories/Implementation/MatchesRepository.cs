using System.Runtime.Intrinsics.X86;
using Dapper;
using frontend.Repositories;
using Npgsql;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class MatchesRepository: IMatchesRepository
{
    private readonly IConfiguration _configuration;
    private readonly string sqlString;
    public MatchesRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        sqlString = _configuration.GetConnectionString("Database");
    }
    
    public void Create(int player1Id, int player2Id, int winnerId)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = "INSERT INTO matches (player1, player2, winner) VALUES (@p1, @p2, @w);";

            connection.Query(query, new { p1 = player1Id, p2 = player2Id, w = winnerId});
        }
    }

    public List<Match> GetAll()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = "SELECT * FROM matches;";

            return connection.Query<Match>(query).ToList();
        }
    }

    public List<Match> GetMatchesByPlayerName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = $"SELECT m.ID, m.Player1, m.Player2, m.Winner FROM matches m'" +
                           $"'INNER JOIN players p ON m.Player1 = p.ID OR m.Player2 = p.ID" +
                           $"WHERE p.Name = @n;";

            return connection.Query<Match>(query, new {n = name}).ToList();
        }
    }
}