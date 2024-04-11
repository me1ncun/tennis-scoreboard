using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Database.Services;
using tennis.Score.Score_system;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly MatchesRepository _matchesRepository;
        public HomeController(IPlayerService playerService)
        {
            _playerService = playerService;
            _matchesRepository = new MatchesRepository();
        }

        [HttpGet("finished-matches")]
        public IActionResult FinishedMatches()
        {
            var matches = _matchesRepository.GetAll();
            
            List<Match> matchesWithName = new List<Match>();
            foreach (var match in matches)
            {
                Match matchChanged = new Match()
                {
                    Player1Name = _matchesRepository.GetNameById(match.Player1),
                    Player2Name = _matchesRepository.GetNameById(match.Player2),
                    WinnerName = _matchesRepository.GetNameById(match.Winner)
                };
                matchesWithName.Add(matchChanged);
            }
            if (matches != null)
            {
                return View(matchesWithName);
            }
            else
            {
                return NotFound();
            }
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
                Player1 = new PlayerDTO()
                {
                    Name = matchDto.Player1Name,
                    Scores = new Point(),
                    Sets = new Set(),
                    Games = new Game()
                },
                Player2 = new PlayerDTO()
                {
                    Name = matchDto.Player2Name,
                    Scores = new Point(),
                    Sets = new Set(),
                    Games = new Game()
                },

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
        public IActionResult MatchScore(string player, string opponent)
        {
            NewMatch match = GetMatch();
            MatchScoreCalculationService _matchScoreCalculationService = new MatchScoreCalculationService(match);
            if (player == "Player1")
            {
                _matchScoreCalculationService.IncreasePlayerScore(match.Player1, match.Player2);
            }
            else if(player == "Player2")
            {
                _matchScoreCalculationService.IncreasePlayerScore(match.Player2, match.Player1);
            }
            
            if (match.Player1.Sets.amount == 2 || match.Player2.Sets.amount == 2)
            {
                // Redirect to another page indicating match result
                return RedirectToAction("FinishedMatches", "Home");
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
