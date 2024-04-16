
using Newtonsoft.Json;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Enums;
using tennis.Score.Score_system;

namespace tennis.Database.Services;

public class MatchScoreCalculationService
{
    private readonly MatchService _matchService;
    private readonly NewMatch _match;
    public MatchScoreCalculationService(NewMatch match, MatchService matchService)
    {
        _match = match;
        _matchService = matchService;
    }
    
    public void AddPointToWinnerOfGame(PlayerDTO winner, NewMatch match) {
        switch (match.State) {
            case State.GAME : {
                if (winner == match.Player1)
                {
                    match.SetPointPlayerOne(IncreasePointPlayerInGame(match.GetPointPlayerOne()));
                }
                else
                {
                    match.SetPointPlayerTwo(IncreasePointPlayerInGame(match.GetPointPlayerTwo()));
                }
                break;
            }
            case State.ADVANTAGE : {
                if (winner == match.Player1)
                {
                    match.SetPointPlayerOne(IncreasePointPlayerInAdvantage(match.GetPointPlayerOne()));
                }
                else
                {
                    match.SetPointPlayerTwo(IncreasePointPlayerInAdvantage(match.GetPointPlayerTwo()));
                }
                    break;
            }
            case State.TIE : {
                if (winner == match.Player1)
                {
                    match.SetPointPlayerOne(IncreasePointPlayerInTie(match.GetPointPlayerOne()));
                }
                else
                {
                    match.SetPointPlayerTwo(IncreasePointPlayerInTie(match.GetPointPlayerTwo()));
                }
                    break;
            }
        }
        UpdateGameScore(match);
        UpdateGameStatus(match);
    }

    private int IncreasePointPlayerInGame(int playerPoint) {
        if (playerPoint < 30) {
            return playerPoint + 15;
        } else {
            return playerPoint + 10;
        }
    }

    private int IncreasePointPlayerInAdvantage(int playerPoint) {
        return playerPoint + 5;
    }

    private int IncreasePointPlayerInTie(int playerPoint) {
        return ++playerPoint;
    }


    private void UpdateGameScore(NewMatch match) {
        switch (match.State) {
            case State.GAME: {
                if (match.GetPointPlayerOne() == 50) {
                    ResetPlayersPoints(match);
                    IncreasePlayerGame(PlayerNumber.PLAYER_ONE, match);
                } else if (match.GetPointPlayerTwo() == 50) {
                    ResetPlayersPoints(match);
                    IncreasePlayerGame(PlayerNumber.PLAYER_TWO, match);
                }
                if (match.GetGamePlayerOne() == 6 && match.GetGamePlayerTwo() < 5) {
                    ResetPlayersGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, match);
                } else if (match.GetGamePlayerOne() == 7 && match.GetGamePlayerTwo() == 5) {
                    ResetPlayersGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, match);
                } else if (match.GetGamePlayerTwo() == 6 && match.GetGamePlayerOne() < 5) {
                    ResetPlayersGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, match);
                } else if (match.GetGamePlayerTwo() == 7 && match.GetGamePlayerOne() == 5) {
                    ResetPlayersGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, match);
                }
                break;
            }
            case State.ADVANTAGE : {
                if (match.GetPointPlayerOne() == 45 && match.GetPointPlayerTwo() == 45) {
                    match.SetPointPlayerOne(40);
                    match.SetPointPlayerTwo(40);
                } else if (match.GetPointPlayerOne() == 50) {
                    ResetPlayersPoints(match);
                    IncreasePlayerGame(PlayerNumber.PLAYER_ONE, match);
                    match.State = State.GAME;
                } else if (match.GetPointPlayerTwo() == 50) {
                    ResetPlayersPoints(match);
                    IncreasePlayerGame(PlayerNumber.PLAYER_TWO, match);
                    match.State = State.GAME;
                }
                break;
            }
            case State.TIE : {
                if (match.GetPointPlayerOne() == 7 && match.GetPointPlayerTwo() <= 5) {
                    ResetPlayersPointsAndGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, match);
                    match.State = State.GAME;
                } else if (match.GetPointPlayerTwo() == 7 && match.GetPointPlayerOne() <= 5) {
                    ResetPlayersPointsAndGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, match);
                    match.State = State.GAME;
                } else if (match.GetPointPlayerOne() >= 7 && (match.GetPointPlayerOne() - match.GetPointPlayerTwo() == 2)) {
                    ResetPlayersPointsAndGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, match);
                    match.State = State.GAME;
                } else if (match.GetPointPlayerTwo() >= 7 && (match.GetPointPlayerTwo() - match.GetPointPlayerOne() == 2)) {
                    ResetPlayersPointsAndGames(match);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, match);
                    match.State = State.GAME;
                }
                break;
            }
        }
    }

    private void UpdateGameStatus(NewMatch match) {
        if (match.GetPointPlayerOne() == 40 && match.GetPointPlayerTwo() == 40) {
            match.State = State.ADVANTAGE;
        } else if (match.GetGamePlayerOne() == 6 && match.GetGamePlayerTwo() == 6) {
            match.State = State.TIE;
        } else if (match.GetSetPlayerOne() == 2 || match.GetSetPlayerTwo() == 2) {
            match.State = State.FINISHED;
            ResetPlayersPointsAndGames(match);
        }
    }

    private void ResetPlayersPoints(NewMatch match) {
        match.SetPointPlayerOne(0);
        match.SetPointPlayerTwo(0);
    }

    private void ResetPlayersGames(NewMatch match) {
        match.SetGamePlayerOne(0);
        match.SetGamePlayerTwo(0);
    }

    private void ResetPlayersPointsAndGames(NewMatch match) {
        match.SetPointPlayerOne(0);
        match.SetPointPlayerTwo(0);
        match.SetGamePlayerOne(0);
        match.SetGamePlayerTwo(0);
    }

    private void IncreasePlayerGame(PlayerNumber playerNumber, NewMatch match) {
        switch (playerNumber) {
            case PlayerNumber.PLAYER_ONE: match.SetGamePlayerOne(match.GetGamePlayerOne() + 1);
                break;

            case PlayerNumber.PLAYER_TWO: match.SetGamePlayerTwo(match.GetGamePlayerTwo() + 1);
                break;
        }
    }

    private void IncreasePlayerSet(PlayerNumber playerNumber, NewMatch match) {
        switch (playerNumber) {
            case PlayerNumber.PLAYER_ONE : match.SetSetPlayerOne(match.GetSetPlayerOne() + 1);
                break;

            case PlayerNumber.PLAYER_TWO : match.SetSetPlayerTwo(match.GetSetPlayerTwo() + 1);
                break;
        }
    }
    
}