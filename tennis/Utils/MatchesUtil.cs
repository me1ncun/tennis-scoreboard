using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Services;
using tennis.DTO;
using tennis.Score.Score_system;

namespace tennis.Utils;

public class MatchesUtil
{
    public async Task<IEnumerable<MatchDTO>> GetToWithNames(IEnumerable<Match> matchesWithId, MatchService _matchService)
    {
        List<MatchDTO> matchesWithName = new List<MatchDTO>();
        foreach (var match in matchesWithId)
        {
            MatchDTO matchChanged = new MatchDTO()
            {
                ID = match.ID,
                Player1 = await _matchService.GetNameById(match.Player1),
                Player2 = await _matchService.GetNameById(match.Player2),
                Winner =  await _matchService.GetNameById(match.Winner)
            };
            matchesWithName.Add(matchChanged);
        }

        return matchesWithName;
    }

    public MatchScoreDTO GetToScoreDTO(Registration registration)
    {
        var match = new MatchScoreDTO
        {
            Id = Guid.NewGuid(),
            Player1 = new PlayerScoreDTO()
            {
                Name = registration.Player1Name,
                Scores = new Point(),
                Sets = new Set(),
                Games = new Game()
            },
            Player2 = new PlayerScoreDTO()
            {
                Name = registration.Player2Name,
                Scores = new Point(),
                Sets = new Set(),
                Games = new Game()
            },
        };
        return match;
    }
}