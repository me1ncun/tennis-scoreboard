﻿
using frontend.Repositories;
using tennis_scoreboard.Models;

public class PlayerService: IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
    public void Register(string name)
    {
        _playerRepository.Register(name);
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