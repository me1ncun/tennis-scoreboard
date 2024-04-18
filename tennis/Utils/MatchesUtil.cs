using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Services;
using tennis.Score.Score_system;

namespace tennis.Utils;

public class MatchesUtil
{
    public List<Match> GetToWithNames(List<Match> matchesWithId, MatchService _matchService)
    {
        List<Match> matchesWithName = new List<Match>();
        foreach (var match in matchesWithId)
        {
            Match matchChanged = new Match()
            {
                ID = match.ID,
                Player1Name = _matchService.GetNameById(match.Player1),
                Player2Name = _matchService.GetNameById(match.Player2),
                WinnerName = _matchService.GetNameById(match.Winner)
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