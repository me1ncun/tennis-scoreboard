using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;

namespace tennis.Database.Services;

public class MatchService
{
    private readonly MatchesRepository _matchesRepository;
    public MatchService(MatchesRepository matchesRepository)
    {
        _matchesRepository = matchesRepository;
    }
    
    public void CreateMatch(PlayerDTO player1, PlayerDTO player2, PlayerDTO winner)
    {
        _matchesRepository.Create(player1, player2, winner);
    }
    
    public IEnumerable<Match> GetAllMatches()
    {
        return _matchesRepository.GetAll();
    }
    
    public Match GetMatchById(Guid id)
    {
        return _matchesRepository.GetById(id);
    }
}