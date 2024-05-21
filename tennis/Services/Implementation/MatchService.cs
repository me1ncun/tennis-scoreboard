using frontend.Repositories;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Utils;

namespace tennis.Database.Services;

public class MatchService
{
    private readonly IMatchesRepository _matchesRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly MatchesUtil _matchesUtil;
    public MatchService(IMatchesRepository matchesRepository, MatchesUtil matchesUtil, IPlayerRepository playerRepository)
    {
        _matchesRepository = matchesRepository;
        _matchesUtil = matchesUtil;
        _playerRepository = playerRepository;
    }
    
    public async Task CreateMatch(PlayerScoreDTO player1, PlayerScoreDTO player2, PlayerScoreDTO winner)
    {
        // get ids of players and winner from db
        var player1Id = await _playerRepository.GetIdByName(player1.Name);
        var player2Id = await _playerRepository.GetIdByName(player2.Name);
        var winnerId = await _playerRepository.GetIdByName(winner.Name);
        
        _matchesRepository.Create(player1Id, player2Id, winnerId);
    }
    
    public async Task<List<Match>> GetAllMatches()
    {
        // get all matches from db and get their id's
        var matches = _matchesRepository.GetAll();
        // add player names to matches
        var matchesWithName = _matchesUtil.GetToWithNames(matches.Result, this);

        return matchesWithName.Result;
    }
    
    public async Task<string> GetNameById(int id)
    {
        return await _playerRepository.GetNameById(id);
    }
    
    public async Task<List<Match>> GetMatchesByPlayerName(string name)
    {
        var matches =  _matchesRepository.GetMatchesByPlayerName(name);
        // Add player names to matches
        var matchesWithName = _matchesUtil.GetToWithNames(matches.Result, this);
        
        
        return matchesWithName.Result;
    }
}