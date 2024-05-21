using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public Task RegisterIfNotExist(string name);
    public Task<IEnumerable<Player>> GetPlayerByName(string name);
    public Task<IEnumerable<string>> GetNameById(int id);
    public Task<IEnumerable<int>> GetIdByName(string name);
}