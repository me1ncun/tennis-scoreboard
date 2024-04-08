using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using tennis_scoreboard.Models;

namespace frontend.Controllers
{
    public class NewMatchController: Controller
    {
        [Route("new-match")]
        public IActionResult Index()
        {
            return View();
        }

        private static Dictionary<Guid, NewMatch> currentMatches = new Dictionary<Guid, NewMatch>();
        private static List<Player> players = new List<Player>();

        [HttpPost]
        public IActionResult NewMatch(string playerName1, string playerName2)
        {
            // Проверка существования игроков в таблице Players
            if (!PlayerExists(playerName1))
                players.Add(new Player { Name = playerName1 });

            if (!PlayerExists(playerName2))
                players.Add(new Player { Name = playerName2 });

            // Создание экземпляра класса Match
            NewMatch newMatch = new NewMatch
            {
                Id = Guid.NewGuid(),
                Player1 = playerName1,
                Player2 = playerName2,
                ScorePlayer1 = 0,
                ScorePlayer2 = 0
            };

            // Добавление матча в коллекцию текущих матчей
            currentMatches.Add(newMatch.Id, newMatch);

            // Редирект на страницу /match-score?uuid=$match_id
            return RedirectToAction("MatchScore", new { uuid = newMatch.Id });
        }
        private bool PlayerExists(string playerName)
        {
            return players.Any(p => p.Name == playerName);
        }
    }
}
