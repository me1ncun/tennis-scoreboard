using frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class NewMatchController: Controller
    {
        [Route("new-match")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
