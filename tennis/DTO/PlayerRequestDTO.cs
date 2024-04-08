namespace tennis_scoreboard.DTO
{
    public class PlayerRequestDTO
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }

        public PlayerRequestDTO(string name1, string name2)
        {
            Name1 = name1;
            Name2 = name2;
        }
    }
}
