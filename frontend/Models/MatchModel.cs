namespace frontend.Models
{
    public class MatchModel
    {
        public Guid MatchId { get; set; }
        public MatchModel()
        {
            MatchId = Guid.NewGuid();
        }
    }
}
