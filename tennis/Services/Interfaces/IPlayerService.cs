
using tennis_scoreboard.Models;

public interface IPlayerService
{
    public Task RegisterIfNotExist(string name);

}