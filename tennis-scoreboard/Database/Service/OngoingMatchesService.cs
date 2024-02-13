using System;
using tennis_scoreboard.Models;

namespace tennis_scoreboard.Service
{
    public class OngoingMatchesService
    {
        private static Dictionary<Guid, Match> ongoingMatches = new Dictionary<Guid, Match>();

        public static void AddMatch(Guid uuid, Match match)
        {
            ongoingMatches.Add(uuid, match);
        }

        public static Match GetMatch(Guid uuid)
        {
            // Check if the key exists before attempting to get the value
            if (ongoingMatches.TryGetValue(uuid, out Match match))
            {
                return match;
            }
            else
            {
                return null; // or throw an exception, depending on your use case
            }
        }

        public static void RemoveMatch(Guid uuid)
        {
            ongoingMatches.Remove(uuid);
        }
    }
}
