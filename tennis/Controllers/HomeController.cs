using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Services;
using tennis.Score.Score_system;

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
        public IActionResult FinishedMatches()
        {
            return View();
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
            _playerService.Register(matchDto.Player1Name);
            _playerService.Register(matchDto.Player2Name);

            var match = new NewMatch
            {
                Id = Guid.NewGuid(),
                Player1 = matchDto.Player1Name,
                Player2 = matchDto.Player2Name,
                ScorePlayer1 = new Point(),
                ScorePlayer2 = new Point(),
                SetPlayer1 = new Set(),
                SetPlayer2 = new Set(),
                GamePlayer1 = new Game(),
                GamePlayer2 = new Game()

            };
            
            string serializedMatch = JsonConvert.SerializeObject(match);
            
            /*_ongoingMatchesService.AddMatch(match);*/
            HttpContext.Session.SetString("MatchId", match.Id.ToString());
            HttpContext.Session.SetString("Match", serializedMatch);

            return RedirectToAction("MatchScore", "Home", new { uuid = match.Id.ToString() });
        }

        [HttpGet("match-score")]
        public IActionResult MatchScore([FromQuery] Guid uuid)
        {
            if (uuid == Guid.Empty)
            {
                return BadRequest("Invalid UUID");
            }
            
            NewMatch match = GetMatch();

            if (match == null)
            {
                return NotFound("Match not found");
            }

            return View(match);
        }
        
        [HttpPost("match-score")]
        public IActionResult MatchScore(string player, int opponentScore)
        {
            NewMatch match = GetMatch();
            MatchScoreCalculationService _matchScoreCalculationService = new MatchScoreCalculationService(match);
            if (player == "Player1")
            {
                _matchScoreCalculationService.IncreasePlayerScore(match.ScorePlayer1, opponentScore);
            }
            else if(player == "Player2")
            {
                _matchScoreCalculationService.IncreasePlayerScore(match.ScorePlayer2, opponentScore);
            }
            
            // save changes to session
            string serializedMatch = JsonConvert.SerializeObject(match);
            HttpContext.Session.SetString("Match", serializedMatch);
            
            return View(match);
        }
        
        // additional methods:
        public NewMatch GetMatch()
        {
            string serializedMatchFromSession = HttpContext.Session.GetString("Match");
            NewMatch match = JsonConvert.DeserializeObject<NewMatch>(serializedMatchFromSession);
            return match;
        }
    }
}
