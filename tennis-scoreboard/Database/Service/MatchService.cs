using tennis_scoreboard.Database.Repository;
using tennis_scoreboard.Models;

namespace tennis_scoreboard.Database.Service
{
    public class MatchService
    {
        private MatchRepository MatchRepository;
        public MatchService(MatchRepository matchRepository)
        {
            MatchRepository = matchRepository;
        }

        public bool PostMatch(Match match)
        {
            var added = MatchRepository.CreateMatch(match);
            return added;
        }
    }
}
