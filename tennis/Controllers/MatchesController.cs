using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class MatchesController: Controller
    {
        [Route("matches")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
