using System.ComponentModel.DataAnnotations.Schema;

namespace tennis_scoreboard.Models
{
    [Table("players")]
    public class Player
    {
        [Column("id")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
