using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using tennis_scoreboard.Database.Repository;
using tennis_scoreboard.Models;
using tennis_scoreboard.Database.Service;
using System.Numerics;
using System.Xml.Linq;
using tennis_scoreboard.Service;

namespace tennis_scoreboard.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private PlayerService PlayerService;
        private MatchService MatchService;
        public PlayerController(PlayerService playerService, MatchService matchService)
        {
            PlayerService = playerService;
            MatchService = matchService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var players = PlayerService.GetAllPlayers();
            if (players != null)
            {
                return new JsonResult(players);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public class PlayerRequest
        {
            public Player player1 { get; set; }
            public Player player2 { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlayerRequest playerRequest)
        {
            if (playerRequest == null)
            {
                return new JsonResult(StatusCodes.Status400BadRequest);
            }
            else
            {
                var post1 = PlayerService.PostPlayer(playerRequest.player1.Name);
                var post2 = PlayerService.PostPlayer(playerRequest.player2.Name);

                List<Player> list = new List<Player>();
                list.Add(PlayerService.GetPlayer(playerRequest.player1.Name));
                list.Add(PlayerService.GetPlayer(playerRequest.player2.Name));

                // You might want to return some data if needed
                Match match = new Match()
                {
                    Player1 = playerRequest.player1,
                    Player2 = playerRequest.player2,
                };

                Guid myuuid = Guid.NewGuid();
                OngoingMatchesService.AddMatch(myuuid, match);

                return RedirectToAction("Index", "MatchScore", new { uuid = myuuid });
            }
        }

        [HttpDelete]
        public IActionResult Delete(string name)
        {
            if(name == null)
            {
                return new JsonResult(StatusCodes.Status400BadRequest);
            }
            else
            {
                PlayerService.DeletePlayer(name);
                return Ok();
            }
        }
    }
}