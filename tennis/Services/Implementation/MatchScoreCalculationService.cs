
using Newtonsoft.Json;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Score.Score_system;

namespace tennis.Database.Services;

public class MatchScoreCalculationService
{
    private readonly MatchesRepository _matchesRepository;
    private readonly NewMatch _match;
    public MatchScoreCalculationService(NewMatch match)
    {
        _match = match;
        _matchesRepository = new MatchesRepository();
    }

    public void IncreasePlayerScore(PlayerDTO player, PlayerDTO oppponent)
    {
        switch (player.Scores.amount)
        {
            case 0:
                player.Scores.IncreaseCounter(15);
                break;
            case 15:
                player.Scores.IncreaseCounter(15);
                break;
            case 30:
                player.Scores.IncreaseCounter(10);
                break;
            case 40:
                CheckMatchWinner(player, oppponent);
                CheckSetWinner(player, oppponent);
                CheckGameWinner(player, oppponent);
                break;
            default: player.Scores.IncreaseCounter(1);
                break;
        }
    }
    public void CheckGameWinner(PlayerDTO player, PlayerDTO oppponent)
    { 
        if(oppponent.Scores.amount == player.Scores.amount && player.Scores.amount == 40) 
        {
            player.Scores.amount = 0;
            _match.Player2.Scores.amount = 0;
            
            player.Scores.IncreaseCounter(1);
        }
        else if (oppponent.Scores.amount == 40)
        {
            oppponent.Games.IncreaseCounter();
            oppponent.Scores.amount = 0;
            _match.Player1.Scores.amount = 0;
        }
        else if (player.Scores.amount == 40)
        {
            player.Games.IncreaseCounter();
            player.Scores.amount = 0;
            _match.Player2.Scores.amount = 0;
        }
    }
    
    public void CheckSetWinner(PlayerDTO player, PlayerDTO oppponent)
    {
        if (player.Games.amount == 6 && oppponent.Games.amount < 5)
        {
            player.Sets.IncreaseCounter();
            player.Games.amount = 0;
            oppponent.Games.amount = 0;
        }
        else if (player.Games.amount == 7 && oppponent.Games.amount == 5)
        {
            player.Sets.IncreaseCounter();
            player.Games.amount = 0;
            oppponent.Games.amount = 0;
        }
    }
    
    public void CheckMatchWinner(PlayerDTO player, PlayerDTO oppponent)
    {
        if (player.Sets.amount == 2)
        {
            _matchesRepository.Create(player, oppponent, player);
            _match.Player1 = player;
        }
        else if (oppponent.Sets.amount == 2)
        {
            _matchesRepository.Create(oppponent, player, oppponent);
            _match.Player2 = oppponent;
        }
    }
    
}