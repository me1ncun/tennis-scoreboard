using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public void Register(string name);
    public bool Exists(string name);
    public List<Player> GetAll();
    public Player GetByName(string name);

}