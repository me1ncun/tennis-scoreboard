using tennis_scoreboard.Database.Repository;
using tennis_scoreboard.Models;

namespace tennis_scoreboard.Database.Service
{
    public class PlayerService
    {
        private PlayerRepository PlayerRepository;
        public PlayerService(PlayerRepository playerRepository) 
        {
            PlayerRepository = playerRepository;
        }

        public List<Player> GetAllPlayers()
        {
            return PlayerRepository.GetAllPlayers();
        }

        public Player GetPlayer(string name)
        {
            var player = GetAllPlayers().FirstOrDefault(player => player.Name == name);

            if (player == null)
            {
                throw new Exception("Player not found");
            }

            return player;
        }

        public int GetPlayerID(Player searchPlayer)
        {
            var foundPlayer = GetAllPlayers().FirstOrDefault(player => player.Name == searchPlayer.Name);

            // Проверка, найден ли игрок
            if (foundPlayer != null)
            {
                return foundPlayer.Id;
            }

            // Возвращаемое значение, если игрок не найден
            return -1;
        }

        public bool PostPlayer(string name)
        {
            var added = PlayerRepository.CreatePlayer(name);
            return added;
        }

        public void DeletePlayer(string name) 
        {
            PlayerRepository.DeletePlayer(name);
        }

    }
}
