using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IMatchesRepository
{
    public void Create(int player1Id, int player2Id, int winnerId);
    public List<Match> GetAll();
    public string GetNameById(int id);
    public int GetIdByName(string name);
    public List<Match> GetMatchesByPlayerName(string name);

}