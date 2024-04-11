using tennis.Score.Score_system;
using Point = System.Drawing.Point;

namespace tennis_scoreboard.DTO;

public class PlayerDTO
{
    public string Name { get; set; }
    public Point Scores { get; set; }
    public Set Sets { get; set; }
    public Game Games { get; set; }
}