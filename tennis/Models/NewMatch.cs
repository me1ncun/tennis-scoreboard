using tennis.Score.Score_system;

namespace tennis_scoreboard.Models
{
    public class NewMatch
    {
        public Guid Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public Point ScorePlayer1 { get; set; }
        public Point ScorePlayer2 { get; set; }
        public Set SetPlayer1 { get; set; } 
        public Set SetPlayer2 { get; set; }
        public Game GamePlayer1 { get; set; }
        public Game GamePlayer2 { get; set; }
    }
}
