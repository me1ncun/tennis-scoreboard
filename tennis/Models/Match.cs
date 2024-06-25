using System.ComponentModel.DataAnnotations.Schema;

namespace tennis_scoreboard.Models
{
    [Table("matches")]
    public class Match
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("player1")]
        public int Player1 { get; set; }
        [Column("player2")]
        public int Player2 { get; set; }
        [Column("winner")] 
        public int Winner { get; set; }
    }
}
