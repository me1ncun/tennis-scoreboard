using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public void RegisterIfNotExist(string name);
    public List<Player> GetAllPlayers();
    public Player GetPlayerByName(string name);

}