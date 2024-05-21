using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public void RegisterIfNotExist(string name);
    public IEnumerable<Player> GetPlayerByName(string name);
    public IEnumerable<string> GetNameById(int id);
    public IEnumerable<int> GetIdByName(string name);
}