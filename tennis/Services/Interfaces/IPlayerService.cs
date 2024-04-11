
using tennis_scoreboard.Models;

public interface IPlayerService
{
    public void Register(string name);
    public List<Player> GetAll();
    public Player GetByName(string name);
    
}