namespace tennis_scoreboard.Models
{
    public class NewMatch
    {
        public Guid Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public int ScorePlayer1 { get; set; }
        public int ScorePlayer2 { get; set; }
    }
}
