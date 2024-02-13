using System.Data.SQLite;
using tennis_scoreboard.Database.Service;
using tennis_scoreboard.Models;

namespace tennis_scoreboard.Database.Repository
{
    public class MatchRepository
    {
        private AppDBContext dbContext;
        private PlayerService PlayerService;
        public MatchRepository(AppDBContext appDBContext, PlayerService playerService)
        {
            dbContext = appDBContext;
            PlayerService = playerService;
        }
        public bool CreateMatch(Match match)
        {
            try
            {
                string query = "INSERT INTO Matches (Player1, Player2) VALUES (@Player1, @Player2)";
                using (var connection = new SQLiteConnection(dbContext.DatabaseSource))
                {
                    // Создаем команду для выполнения запроса
                    using (var command = new SQLiteCommand(connection))
                    {
                        connection.Open();

                        // Устанавливаем параметры команды
                        command.Parameters.AddWithValue("Player1", PlayerService.GetPlayerID(match.Player1));
                        command.Parameters.AddWithValue("Player2", PlayerService.GetPlayerID(match.Player2));
                        command.CommandText = query;

                        // Выполняем запрос
                        command.ExecuteNonQuery();

                        connection.Close(); // Закрываем соединение с базой данных
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
