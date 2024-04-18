using tennis.Score.Score_system;

namespace tennis_scoreboard.DTO;

public class PlayerScoreDTO
{
    public string Name { get; set; }
    public Point Scores { get; set; }
    public Set Sets { get; set; }
    public Game Games { get; set; }
}