using tennis_scoreboard.DTO;
using tennis.Enums;
using tennis.Score.Score_system;

namespace tennis_scoreboard.Models
{
    public class MatchScoreDTO
    {
        public Guid Id { get; set; }
        public PlayerScoreDTO Player1 { get; set; }
        public PlayerScoreDTO Player2 { get; set; }
        public State State { get; set; }
        
        public MatchScoreDTO()
        {
            this.Player1 = new PlayerScoreDTO();
            this.Player2 = new PlayerScoreDTO();
            this.State = State.GAME;
        }

        public void SetGamePlayerOne(int point)
        {
            this.Player1.Games.amount = point;
        }

        public void SetGamePlayerTwo(int point)
        {
            this.Player2.Games.amount = point;
        }

        public void SetPointPlayerOne(int point)
        {
            this.Player1.Scores.amount = point;
        }

        public void SetPointPlayerTwo(int point)
        {
            this.Player2.Scores.amount = point;
        }
        
        public void SetSetPlayerOne(int point)
        {
            this.Player1.Sets.amount = point;
        }
        
        public void SetSetPlayerTwo(int point)
        {
            this.Player2.Sets.amount = point;
        }
        
        public int GetPointPlayerOne()
        {
            return this.Player1.Scores.amount;
        }
        public int GetPointPlayerTwo()
        {
            return this.Player2.Scores.amount;
        }
        
        public int GetGamePlayerOne()
        {
            return this.Player1.Games.amount;
        }
        public int GetGamePlayerTwo()
        {
            return this.Player2.Games.amount;
        }
        public int GetSetPlayerOne()
        {
            return this.Player1.Sets.amount;
        }
        public int GetSetPlayerTwo()
        {
            return this.Player2.Sets.amount;
        }
    }
}