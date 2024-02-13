using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using tennis_scoreboard.Database.Repository;
using tennis_scoreboard.Models;
using tennis_scoreboard.Database.Service;
using System.Numerics;
using System.Xml.Linq;
using System;
using tennis_scoreboard.Service;

namespace tennis_scoreboard.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MatchScoreController : ControllerBase
    {
        [HttpGet("/match-score")]
        public IActionResult GetMatchScore([FromQuery] string uuid)
        {
            Guid guid = Guid.Parse(uuid);
            if (string.IsNullOrEmpty(uuid))
            {
                return BadRequest("UUID is required");
            }

            // Теперь у вас есть uuid, который можно использовать для вызова OngoingMatchesService.GetMatch
            Match match = OngoingMatchesService.GetMatch(guid);

            if (match == null)
            {
                return NotFound("Match not found");
            }

            // Тут вы можете работать с полученным матчем

            return Ok($"Match score for {uuid}: {match.Player1}");
        }
    }
}