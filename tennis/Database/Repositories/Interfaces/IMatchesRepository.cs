using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IMatchesRepository
{
    public void Create(Player player1, Player player2, Player winner);
    public List<Match> GetAll();
    public Match GetById(Guid id);

}