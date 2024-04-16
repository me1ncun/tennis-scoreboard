using frontend.Repositories;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;

namespace tennis.Database.Services;

public class MatchService
{
    private readonly IMatchesRepository _matchesRepository;
    public MatchService(IMatchesRepository matchesRepository)
    {
        _matchesRepository = matchesRepository;
    }
    
    public void CreateMatch(PlayerDTO player1, PlayerDTO player2, PlayerDTO winner)
    {
        var player1Id = _matchesRepository.GetIdByName(player1.Name);
        var player2Id = _matchesRepository.GetIdByName(player2.Name);
        var winnerId = _matchesRepository.GetIdByName(winner.Name);
        _matchesRepository.Create(player1Id, player2Id, winnerId);
    }
    
    public List<Match> GetAllMatches()
    {
        var matches = _matchesRepository.GetAll();
        // Add player names to matches
        List<Match> matchesWithName = new List<Match>();
        foreach (var match in matches)
        {
            Match matchChanged = new Match()
            {
                Player1Name = GetNameById(match.Player1),
                Player2Name = GetNameById(match.Player2),
                WinnerName = GetNameById(match.Winner)
            };
            matchesWithName.Add(matchChanged);
        }

        return matchesWithName;
    }
    
    public Match GetMatchById(Guid id)
    {
        return _matchesRepository.GetMatchByGuid(id);
    }
    
    public int GetIdByName(string name)
    {
        return _matchesRepository.GetIdByName(name);
    }
    
    public string GetNameById(int id)
    {
        return _matchesRepository.GetNameById(id);
    }
    
    public List<Match> GetMatchesByPlayerName(string name)
    {
        var matches =  _matchesRepository.GetMatchesByPlayerName(name);
        // Add player names to matches
        List<Match> matchesWithName = new List<Match>();
        foreach (var match in matches)
        {
            Match matchChanged = new Match()
            {
                Player1Name = GetNameById(match.Player1),
                Player2Name = GetNameById(match.Player2),
                WinnerName = GetNameById(match.Winner)
            };
            matchesWithName.Add(matchChanged);
        }

        return matchesWithName;
    }
}