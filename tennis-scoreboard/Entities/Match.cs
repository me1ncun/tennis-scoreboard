namespace tennis_scoreboard.Models
{
    public class Match
    {
        public int Id { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player Winner { get; set; }

        public Match()
        {

        }
    }
}
