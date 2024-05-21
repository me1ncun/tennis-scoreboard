using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IMatchesRepository
{
    public Task Create(int player1Id, int player2Id, int winnerId);
    public Task<List<Match>> GetAll();
    public Task<List<Match>> GetMatchesByPlayerName(string name);

}