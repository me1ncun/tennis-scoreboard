using Microsoft.AspNetCore.Mvc;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Services;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlayerService _playerService;

        public HomeController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
        
        [HttpGet("new-match")]
        public IActionResult NewMatch()
        {
            return View();
        }

        [HttpPost("new-match")]
        public IActionResult NewMatch(MatchDTO matchDto)
        {
            if (_playerService.Exists(matchDto.Player1Name) == false)
            {
                _playerService.Register(matchDto.Player1Name);
            }
            if (_playerService.Exists(matchDto.Player2Name) == false)
            {
                _playerService.Register(matchDto.Player2Name);
            }

            var match = new NewMatch
            {
                Id = Guid.NewGuid(),
                Player1 = matchDto.Player1Name,
                Player2 = matchDto.Player2Name,
                ScorePlayer1 = 0,
                ScorePlayer2 = 0
            };

            OngoingMatchesService ongoingMatchesService = new OngoingMatchesService();
            ongoingMatchesService.AddMatch(match);

            return RedirectToAction("MatchScore", "Home", new {uuid = match.Id});
        }

        [HttpGet]
        public IActionResult FinishedMatches()
        {
            return View();
        }
        
        [HttpGet("match-score/uuid={uuid}")]
        public IActionResult MatchScore()
        {
            return View();
        }
    }
}
