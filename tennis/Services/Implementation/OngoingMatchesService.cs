using tennis_scoreboard.Models;

namespace tennis.Database.Services;

public class OngoingMatchesService
{
    private Dictionary<Guid, NewMatch> currentMatches;

    public OngoingMatchesService()
    {
        currentMatches = new Dictionary<Guid, NewMatch>();
    }

    public void AddMatch(NewMatch match)
    {
        currentMatches.Add(match.Id, match);
    }

    public NewMatch GetMatch(Guid uuid)
    {
        if (currentMatches.TryGetValue(uuid, out NewMatch match))
        {
            return match;
        }

        return null;
    }

    public void DeleteMatch(Guid uuid)
    {
        currentMatches.Remove(uuid);
    }
}