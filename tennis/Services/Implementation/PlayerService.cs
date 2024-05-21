
using frontend.Repositories;
using tennis_scoreboard.Models;

public class PlayerService: IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
    public async Task RegisterIfNotExist(string name)
    {
        await _playerRepository.RegisterIfNotExist(name);
    }
}