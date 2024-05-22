using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public void RegisterIfNotExist(string name);
    public Task<Player> GetPlayerByName(string name);
    public Task<string> GetNameById(int id);
    public Task<int> GetIdByName(string name);
}