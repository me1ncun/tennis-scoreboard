using Microsoft.AspNetCore.Mvc;
using tennis_scoreboard.Service;
using tennis_scoreboard.Models;

namespace frontend.Controllers
{
    public class MatchScoreController : Controller
    {
        [Route("match-score")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
