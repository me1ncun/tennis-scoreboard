namespace tennis_scoreboard.DTO
{
    public class MatchDTO
    {
        public int ID { get; set; }
        public int? Player1ID { get; set; }
        public int? Player2ID { get; set;}
        public int? WinnerID { get; set;}
    }
}