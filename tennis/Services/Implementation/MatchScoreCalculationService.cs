
using Newtonsoft.Json;
using tennis_scoreboard.Models;
using tennis.Score.Score_system;

namespace tennis.Database.Services;

public class MatchScoreCalculationService
{
    private readonly NewMatch _match;
    public MatchScoreCalculationService(NewMatch match)
    {
        _match = match;
    }

    public void IncreasePlayerScore(Point player, int oppponentScore)
    {
        switch (player.amount)
        {
            case 0:
                player.IncreaseCounter(15);
                break;
            case 15:
                player.IncreaseCounter(15);
                break;
            case 30:
                player.IncreaseCounter(10);
                break;
            case 40: 
                CheckWinner(player, oppponentScore);
                break;
            default: player.IncreaseCounter(1);
                break;
        }
    }
    public void CheckWinner(Point playerPoint, int oppponentScore)
    { 
        if (playerPoint.amount == 40)
        {
            _match.GamePlayer1.IncreaseCounter();
            playerPoint.amount = 0;
            _match.ScorePlayer2.amount = 0;
        }
    }
    
    
}