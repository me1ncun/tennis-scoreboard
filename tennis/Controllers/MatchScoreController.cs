using Microsoft.AspNetCore.Mvc;
using tennis_scoreboard.Service;
using tennis_scoreboard.Models;
using System;

namespace frontend.Controllers
{
    public class MatchScoreController : Controller
    {
        [Route("match-score")]
        public IActionResult Index()
        {
            return View();
        }

        private static Dictionary<Guid, NewMatch> currentMatches = new Dictionary<Guid, NewMatch>();
        private static List<Player> players = new List<Player>();

        [HttpGet]
        public IActionResult NewMatch()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MatchScore(Guid uuid)
        {
            // Получение информации о матче
            if (currentMatches.TryGetValue(uuid, out NewMatch match))
            {
                return View(match);
            }

            // Обработка ошибки, если матч не найден
            return RedirectToAction("NewMatch");
        }

        private bool PlayerExists(string playerName)
        {
            return players.Any(p => p.Name == playerName);
        }
    }
}
