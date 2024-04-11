using tennis_scoreboard.DTO;
using tennis.Score.Score_system;

namespace tennis_scoreboard.Models
{
    public class NewMatch
    {
        public Guid Id { get; set; }
        public PlayerDTO Player1 { get; set; }
        public PlayerDTO Player2 { get; set; }
    }
}
