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
    
    public async Task Create(int player1Id, int player2Id, int winnerId)
    {
        using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
        {
            await sqlCon.OpenAsync();
            string cmdString = $"INSERT INTO matches (\"player1\", \"player2\", \"winner\") VALUES (@p1, @p2, @w)";
        
            using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
            {
                sqlCmd.Parameters.AddWithValue("p1", player1Id);
                sqlCmd.Parameters.AddWithValue("p2", player2Id);
                sqlCmd.Parameters.AddWithValue("w", winnerId);

                await sqlCmd.ExecuteNonQueryAsync();
            } 
        }
    }

    public async Task<List<Match>> GetAll()
    {
        using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
        {
            await sqlCon.OpenAsync();
            string cmdString = $"SELECT * FROM matches";

            using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
            {
                var list = new List<Match>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var match = new Match
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("id")),
                            Player1 = reader.GetInt32(reader.GetOrdinal("player1")),
                            Player2 = reader.GetInt32(reader.GetOrdinal("player2")),
                            Winner = reader.GetInt32(reader.GetOrdinal("winner")),
                        };
                        list.Add(match);
                    }
                }

                return list;
            }
        }
    }

    public async Task<List<Match>> GetMatchesByPlayerName(string name)
    {
        using (NpgsqlConnection sqlCon = new NpgsqlConnection(sqlString))
        {
            await sqlCon.OpenAsync();
            string cmdString = $"SELECT m.ID, m.Player1, m.Player2, m.Winner FROM matches m'" +
                               $"'INNER JOIN players p ON m.Player1 = p.ID OR m.Player2 = p.ID" +
                               $"WHERE p.Name = @n;";

            using (NpgsqlCommand sqlCmd = new NpgsqlCommand(cmdString, sqlCon))
            {
                var list = new List<Match>();
                using (var reader = await sqlCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var match = new Match
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("id")),
                            Player1 = reader.GetInt32(reader.GetOrdinal("player1")),
                            Player2 = reader.GetInt32(reader.GetOrdinal("player2")),
                            Winner = reader.GetInt32(reader.GetOrdinal("winner")),
                        };
                        list.Add(match);
                    }
                }

                return list;
            }
        }
    }
}