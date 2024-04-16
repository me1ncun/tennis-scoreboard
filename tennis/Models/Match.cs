namespace tennis_scoreboard.Models
{
    public class Match
    {
        public int ID { get; set; }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int Winner { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string WinnerName { get; set; }
        
    }
}
