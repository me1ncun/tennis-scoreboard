using frontend.Repositories;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Utils;

namespace tennis.Database.Services;

public class MatchService
{
    private readonly IMatchesRepository _matchesRepository;
    private readonly MatchesUtil _matchesUtil;
    public MatchService(IMatchesRepository matchesRepository, MatchesUtil matchesUtil)
    {
        _matchesRepository = matchesRepository;
        _matchesUtil = matchesUtil;
    }
    
    public void CreateMatch(PlayerScoreDTO player1, PlayerScoreDTO player2, PlayerScoreDTO winner)
    {
        // get ids of players and winner from db
        var player1Id = _matchesRepository.GetIdByName(player1.Name);
        var player2Id = _matchesRepository.GetIdByName(player2.Name);
        var winnerId = _matchesRepository.GetIdByName(winner.Name);
        
        _matchesRepository.Create(player1Id, player2Id, winnerId);
    }
    
    public List<Match> GetAllMatches()
    {
        // get all matches from db and get their id's
        var matches = _matchesRepository.GetAll();
        // add player names to matches
        List<Match> matchesWithName = _matchesUtil.GetToWithNames(matches, this);

        return matchesWithName;
    }
    
    public string GetNameById(int id)
    {
        return _matchesRepository.GetNameById(id);
    }
    
    public List<Match> GetMatchesByPlayerName(string name)
    {
        var matches =  _matchesRepository.GetMatchesByPlayerName(name);
        // Add player names to matches
        List<Match> matchesWithName = _matchesUtil.GetToWithNames(matches, this);
        
        
        return matchesWithName;
    }
}