using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public void Register(string name);
    public List<Player> GetAllPlayers();
    public Player GetPlayerByName(string name);

}