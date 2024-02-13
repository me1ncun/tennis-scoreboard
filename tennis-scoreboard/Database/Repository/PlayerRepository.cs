using System.Configuration;
using System.Data.SQLite;
using System.Xml.Linq;
using tennis_scoreboard.Models;

namespace tennis_scoreboard.Database.Repository
{
    public class PlayerRepository
    {
        private AppDBContext dbContext;
        public PlayerRepository(AppDBContext appDBContext)
        {
            dbContext = appDBContext;
        }

        public bool CreatePlayer(string name)
        {
            try
            {
                // Проверяем существование игрока с заданным именем
                if (PlayerExists(name))
                {
                    // Игрок с таким именем уже существует, вернем false или выполните другие действия
                    return false;
                }

                // Если игрок не существует, то создаем его
                string query = "INSERT INTO Players (Name) VALUES (@Name)";
                using (var connection = new SQLiteConnection(dbContext.DatabaseSource))
                {
                    // Создаем команду для выполнения запроса
                    using (var command = new SQLiteCommand(connection))
                    {
                        connection.Open();

                        // Устанавливаем параметры команды
                        command.Parameters.AddWithValue("Name", name);
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

        // Метод для проверки существования игрока с заданным именем
        private bool PlayerExists(string name)
        {
            string query = "SELECT COUNT(*) FROM Players WHERE Name = @Name";
            using (var connection = new SQLiteConnection(dbContext.DatabaseSource))
            {
                // Создаем команду для выполнения запроса
                using (var command = new SQLiteCommand(query, connection))
                {
                    connection.Open();

                    // Устанавливаем параметры команды
                    command.Parameters.AddWithValue("Name", name);

                    // Выполняем запрос и возвращаем результат
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close(); // Закрываем соединение с базой данных

                    return count > 0;
                }
            }
        }

        public void DeletePlayer(string name)
        {
            try
            {
                // Если игрок не существует, то создаем его
                string query = "DELETE FROM Players WHERE Name = @Name";
                using (var connection = new SQLiteConnection(dbContext.DatabaseSource))
                {
                    // Создаем команду для выполнения запроса
                    using (var command = new SQLiteCommand(connection))
                    {
                        connection.Open();

                        // Устанавливаем параметры команды
                        command.Parameters.AddWithValue("Name", name);
                        command.CommandText = query;

                        // Выполняем запрос
                        command.ExecuteNonQuery();

                        connection.Close(); // Закрываем соединение с базой данных
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public List<Player> GetAllPlayers()
        {
            try
            {
                List<Player> list = new List<Player>();
                string query = "SELECT * FROM Players";
                using (var connection = new SQLiteConnection(dbContext.DatabaseSource))
                {
                    // Create a database command
                    using (var command = new SQLiteCommand(connection))
                    {
                        connection.Open();

                        // Create the table
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Player player = new Player
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                };
                                list.Add(player);
                            }
                        }
                        connection.Close(); // Close the connection to the database
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }


        //public int GetPlayerID(Player player)
        //{
        //    try
        //    {
        //        // Если игрок не существует, то создаем его
        //        string query = "SELECT * FROM Players WHERE Name = @Name";
        //        using (var connection = new SQLiteConnection(dbContext.DatabaseSource))
        //        {
        //            // Создаем команду для выполнения запроса
        //            using (var command = new SQLiteCommand(connection))
        //            {
        //                connection.Open();

        //                // Устанавливаем параметры команды
        //                command.Parameters.AddWithValue("Name", player.Name);
        //                command.CommandText = query;

        //                // Выполняем запрос
        //                command.ExecuteNonQuery();

        //                connection.Close(); // Закрываем соединение с базой данных
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException(ex.Message, ex);
        //    }
        //}
    }
}
