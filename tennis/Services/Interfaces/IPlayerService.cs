
using tennis_scoreboard.Models;

public interface IPlayerService
{
    public void RegisterIfNotExist(string name);
    public List<Player> GetAll();
    public Player GetByName(string name);
    
}