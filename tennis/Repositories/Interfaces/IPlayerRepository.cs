using tennis_scoreboard.Models;

namespace frontend.Repositories;

public interface IPlayerRepository
{
    public Task RegisterIfNotExist(string name);
    public Task<int> GetIdByName(string name);
    public Task<string> GetNameById(int id);

}