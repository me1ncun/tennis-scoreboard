
using frontend.Repositories;
using tennis_scoreboard.Models;

public class PlayerService: IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
    public void RegisterIfNotExist(string name)
    {
        _playerRepository.RegisterIfNotExist(name);
    }
    
    public List<Player> GetAll()
    {
        return _playerRepository.GetAllPlayers();
    }
    
    public Player GetByName(string name)
    {
        return _playerRepository.GetPlayerByName(name);
    }
}