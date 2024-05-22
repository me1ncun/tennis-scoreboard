using System.Runtime.Intrinsics.X86;
using Dapper;
using frontend.Repositories;
using Npgsql;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace tennis.Database.Repositories.Implementation;

public class MatchesRepository : IMatchesRepository
{
    private readonly IConfiguration _configuration;
    private readonly string sqlString;

    public MatchesRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        sqlString = _configuration.GetConnectionString("Database");
    }

    public async void Create(int player1Id, int player2Id, int winnerId)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query =
                """INSERT INTO matches (player1, player2, winner) VALUES (@player1Id, @player2Id, @winnerId);""";

            await connection.QueryFirstOrDefaultAsync(query, new { player1Id, player2Id, winnerId });
        }
    }

    public async Task<IEnumerable<Match>> GetAll()
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = """SELECT * FROM matches;""";

            return await connection.QueryAsync<Match>(query);
        }
    }

    public async Task<IEnumerable<Match>> GetMatchesByPlayerName(string name)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(sqlString))
        {
            string query = @"
            SELECT m.ID, m.Player1, m.Player2, m.Winner FROM matches m INNER JOIN players p ON m.Player1 = p.ID OR m.Player2 = p.ID WHERE p.Name = @name;";

            return await connection.QueryAsync<Match>(query, new { name });
        }
    }
}