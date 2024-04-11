using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IMatchesRepository
{
    public void Create(PlayerDTO player1, PlayerDTO player2, PlayerDTO winner);
    public IEnumerable<Match> GetAll();
    public Match GetById(Guid id);

}