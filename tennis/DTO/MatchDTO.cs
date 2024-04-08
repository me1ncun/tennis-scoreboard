using System.ComponentModel.DataAnnotations;

namespace tennis_scoreboard.DTO
{
    public class MatchDTO
    {
        [Required(ErrorMessage = "Player1 Name is required.")]
        public string Player1Name { get; set; }
        [Required(ErrorMessage = "Player2 Name is required.")]
        public string Player2Name { get; set; }
        
    }
}