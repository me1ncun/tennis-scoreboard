
using Newtonsoft.Json;
using tennis_scoreboard.DTO;
using tennis_scoreboard.Models;
using tennis.Database.Repositories.Implementation;
using tennis.Enums;
using tennis.Score.Score_system;

namespace tennis.Database.Services;

public class MatchScoreCalculationService
{
    public void AddPointToWinnerOfGame(PlayerScoreDTO winner, MatchScoreDTO matchScoreDto) {
        switch (matchScoreDto.State) {
            case State.GAME : {
                if (winner == matchScoreDto.Player1)
                {
                    matchScoreDto.SetPointPlayerOne(IncreasePointPlayerInGame(matchScoreDto.GetPointPlayerOne()));
                }
                else
                {
                    matchScoreDto.SetPointPlayerTwo(IncreasePointPlayerInGame(matchScoreDto.GetPointPlayerTwo()));
                }
                break;
            }
            case State.ADVANTAGE : {
                if (winner == matchScoreDto.Player1)
                {
                    matchScoreDto.SetPointPlayerOne(IncreasePointPlayerInAdvantage(matchScoreDto.GetPointPlayerOne()));
                }
                else
                {
                    matchScoreDto.SetPointPlayerTwo(IncreasePointPlayerInAdvantage(matchScoreDto.GetPointPlayerTwo()));
                }
                    break;
            }
            case State.TIE : {
                if (winner == matchScoreDto.Player1)
                {
                    matchScoreDto.SetPointPlayerOne(IncreasePointPlayerInTie(matchScoreDto.GetPointPlayerOne()));
                }
                else
                {
                    matchScoreDto.SetPointPlayerTwo(IncreasePointPlayerInTie(matchScoreDto.GetPointPlayerTwo()));
                }
                    break;
            }
        }
        UpdateGameScore(matchScoreDto);
        UpdateGameStatus(matchScoreDto);
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


    private void UpdateGameScore(MatchScoreDTO matchScoreDto) {
        switch (matchScoreDto.State) {
            case State.GAME: {
                if (matchScoreDto.GetPointPlayerOne() == 50) {
                    ResetPlayersPoints(matchScoreDto);
                    IncreasePlayerGame(PlayerNumber.PLAYER_ONE, matchScoreDto);
                } else if (matchScoreDto.GetPointPlayerTwo() == 50) {
                    ResetPlayersPoints(matchScoreDto);
                    IncreasePlayerGame(PlayerNumber.PLAYER_TWO, matchScoreDto);
                }
                if (matchScoreDto.GetGamePlayerOne() == 6 && matchScoreDto.GetGamePlayerTwo() < 5) {
                    ResetPlayersGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, matchScoreDto);
                } else if (matchScoreDto.GetGamePlayerOne() == 7 && matchScoreDto.GetGamePlayerTwo() == 5) {
                    ResetPlayersGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, matchScoreDto);
                } else if (matchScoreDto.GetGamePlayerTwo() == 6 && matchScoreDto.GetGamePlayerOne() < 5) {
                    ResetPlayersGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, matchScoreDto);
                } else if (matchScoreDto.GetGamePlayerTwo() == 7 && matchScoreDto.GetGamePlayerOne() == 5) {
                    ResetPlayersGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, matchScoreDto);
                }
                break;
            }
            case State.ADVANTAGE : {
                if (matchScoreDto.GetPointPlayerOne() == 45 && matchScoreDto.GetPointPlayerTwo() == 45) {
                    matchScoreDto.SetPointPlayerOne(40);
                    matchScoreDto.SetPointPlayerTwo(40);
                } else if (matchScoreDto.GetPointPlayerOne() == 50) {
                    ResetPlayersPoints(matchScoreDto);
                    IncreasePlayerGame(PlayerNumber.PLAYER_ONE, matchScoreDto);
                    matchScoreDto.State = State.GAME;
                } else if (matchScoreDto.GetPointPlayerTwo() == 50) {
                    ResetPlayersPoints(matchScoreDto);
                    IncreasePlayerGame(PlayerNumber.PLAYER_TWO, matchScoreDto);
                    matchScoreDto.State = State.GAME;
                }
                break;
            }
            case State.TIE : {
                if (matchScoreDto.GetPointPlayerOne() == 7 && matchScoreDto.GetPointPlayerTwo() <= 5) {
                    ResetPlayersPointsAndGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, matchScoreDto);
                    matchScoreDto.State = State.GAME;
                } else if (matchScoreDto.GetPointPlayerTwo() == 7 && matchScoreDto.GetPointPlayerOne() <= 5) {
                    ResetPlayersPointsAndGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, matchScoreDto);
                    matchScoreDto.State = State.GAME;
                } else if (matchScoreDto.GetPointPlayerOne() >= 7 && (matchScoreDto.GetPointPlayerOne() - matchScoreDto.GetPointPlayerTwo() == 2)) {
                    ResetPlayersPointsAndGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_ONE, matchScoreDto);
                    matchScoreDto.State = State.GAME;
                } else if (matchScoreDto.GetPointPlayerTwo() >= 7 && (matchScoreDto.GetPointPlayerTwo() - matchScoreDto.GetPointPlayerOne() == 2)) {
                    ResetPlayersPointsAndGames(matchScoreDto);
                    IncreasePlayerSet(PlayerNumber.PLAYER_TWO, matchScoreDto);
                    matchScoreDto.State = State.GAME;
                }
                break;
            }
        }
    }

    private void UpdateGameStatus(MatchScoreDTO matchScoreDto) {
        if (matchScoreDto.GetPointPlayerOne() == 40 && matchScoreDto.GetPointPlayerTwo() == 40) {
            matchScoreDto.State = State.ADVANTAGE;
        } else if (matchScoreDto.GetGamePlayerOne() == 6 && matchScoreDto.GetGamePlayerTwo() == 6) {
            matchScoreDto.State = State.TIE;
        } else if (matchScoreDto.GetSetPlayerOne() == 2 || matchScoreDto.GetSetPlayerTwo() == 2) {
            matchScoreDto.State = State.FINISHED;
            ResetPlayersPointsAndGames(matchScoreDto);
        }
    }

    private void ResetPlayersPoints(MatchScoreDTO matchScoreDto) {
        matchScoreDto.SetPointPlayerOne(0);
        matchScoreDto.SetPointPlayerTwo(0);
    }

    private void ResetPlayersGames(MatchScoreDTO matchScoreDto) {
        matchScoreDto.SetGamePlayerOne(0);
        matchScoreDto.SetGamePlayerTwo(0);
    }

    private void ResetPlayersPointsAndGames(MatchScoreDTO matchScoreDto) {
        matchScoreDto.SetPointPlayerOne(0);
        matchScoreDto.SetPointPlayerTwo(0);
        matchScoreDto.SetGamePlayerOne(0);
        matchScoreDto.SetGamePlayerTwo(0);
    }

    private void IncreasePlayerGame(PlayerNumber playerNumber, MatchScoreDTO matchScoreDto) {
        switch (playerNumber) {
            case PlayerNumber.PLAYER_ONE: matchScoreDto.SetGamePlayerOne(matchScoreDto.GetGamePlayerOne() + 1);
                break;

            case PlayerNumber.PLAYER_TWO: matchScoreDto.SetGamePlayerTwo(matchScoreDto.GetGamePlayerTwo() + 1);
                break;
        }
    }

    private void IncreasePlayerSet(PlayerNumber playerNumber, MatchScoreDTO matchScoreDto) {
        switch (playerNumber) {
            case PlayerNumber.PLAYER_ONE : matchScoreDto.SetSetPlayerOne(matchScoreDto.GetSetPlayerOne() + 1);
                break;

            case PlayerNumber.PLAYER_TWO : matchScoreDto.SetSetPlayerTwo(matchScoreDto.GetSetPlayerTwo() + 1);
                break;
        }
    }
    
}