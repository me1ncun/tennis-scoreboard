using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Services;
using tennis.Score.Score_system;
using tennis.Utils;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly MatchService _matchService;
        private readonly MatchesUtil _matchesUtil;
        private readonly MatchScoreCalculationService _matchScoreCalculationService;
        private readonly AppDbContext _context;

        public HomeController(IPlayerService playerService, MatchService matchService, MatchesUtil matchesUtil, AppDbContext context)
        {
            _context = context;
            _playerService = playerService;
            _matchService = matchService;
            _matchesUtil = matchesUtil;
            _matchScoreCalculationService = new MatchScoreCalculationService();
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
        public IActionResult NewMatch(Registration registration)
        {
            // register players if they don't exist
            _playerService.RegisterIfNotExist(registration.Player1Name);
            _playerService.RegisterIfNotExist(registration.Player2Name);

            // create match object
            var match = _matchesUtil.GetToScoreDTO(registration);

            // serialized match
            string serializedMatch = JsonConvert.SerializeObject(match);

            // save match and matchId to session
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

            MatchScoreDTO matchScoreDto = GetCurrentMatch();

            if (matchScoreDto == null)
            {
                return NotFound("Match not found");
            }

            return View(matchScoreDto);
        }

        [HttpPost("match-score")]
        public IActionResult MatchScore(string selectedPlayer, [FromQuery] Guid uuid)
        {
            MatchScoreDTO matchScoreDto = GetCurrentMatch();

            // Add a point to the winner of the game
            if (selectedPlayer == "Player1")
            {
                _matchScoreCalculationService.AddPointToWinnerOfGame(matchScoreDto.Player1, matchScoreDto);
            }
            else if (selectedPlayer == "Player2")
            {
                _matchScoreCalculationService.AddPointToWinnerOfGame(matchScoreDto.Player2, matchScoreDto);
            }

            // check if match is finished
            if (matchScoreDto.Player1.Sets.amount == 2 || matchScoreDto.Player2.Sets.amount == 2)
            {
                // set winner
                PlayerScoreDTO winnerScoreDto = matchScoreDto.Player1.Sets.amount == 2
                    ? matchScoreDto.Player1
                    : matchScoreDto.Player2;

                // create match in db
                _matchService.CreateMatch(winnerScoreDto,
                    winnerScoreDto == matchScoreDto.Player1 ? matchScoreDto.Player2 : matchScoreDto.Player1,
                    winnerScoreDto);

                // render final score
                return RedirectToAction("FinishedMatches", "Home");
            }

            // Save changes to session
            string serializedMatch = JsonConvert.SerializeObject(matchScoreDto);
            HttpContext.Session.SetString("Match", serializedMatch);

            // get guid match from session
            var guid = HttpContext.Session.GetString(matchScoreDto.Id.ToString());
            return RedirectToAction("MatchScore", "Home", new { uuid = matchScoreDto.Id.ToString() });
        }

        [HttpGet("finished-matches")]
        public IActionResult FinishedMatches(int page = 1, string filter_by_player_name = "")
        {
            const int PageSize = 5;
            int totalMatchesCount = _matchService.GetAllMatches().Count;
            int totalPages = (totalMatchesCount + PageSize - 1) / PageSize;

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;
            ViewData["PlayerName"] = filter_by_player_name;

            List<Match> matches;
            matches =  _matchService.GetAllMatches().Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return View(matches);
        }

        [HttpPost("finished-matches")]
        public IActionResult FinishedMatches(string searchedPlayer, int page = 1, string filter_by_player_name = "")
        {
            const int PageSize = 5;
            int totalMatchesCount = _matchService.GetMatchesByPlayerName(searchedPlayer).Count;
            int totalPages = (totalMatchesCount + PageSize - 1) / PageSize;

            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;
            ViewData["PlayerName"] = searchedPlayer;

            List<Match> matches;
            matches = _matchService.GetMatchesByPlayerName(searchedPlayer).Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return View(matches);
        }

        // additional method
        public MatchScoreDTO GetCurrentMatch()
        {
            string serializedMatchFromSession = HttpContext.Session.GetString("Match");
            MatchScoreDTO matchScoreDto = JsonConvert.DeserializeObject<MatchScoreDTO>(serializedMatchFromSession);
            return matchScoreDto;
        }
    }
}