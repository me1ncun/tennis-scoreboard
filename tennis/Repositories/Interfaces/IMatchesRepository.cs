using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IMatchesRepository
{
    public void Create(int player1Id, int player2Id, int winnerId);
    public Task<IEnumerable<Match>> GetAll();
    public Task<IEnumerable<Match>> GetMatchesByPlayerName(string name);
}