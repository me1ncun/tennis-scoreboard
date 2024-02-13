using System;
using System.Data.SQLite;
using tennis_scoreboard.Models;

namespace tennis_scoreboard.Database
{
    public class AppDBContext
    {
        private const string CreatePlayersTableQuery = @"CREATE TABLE IF NOT EXISTS Players (
                                               ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                               Name VARCHAR(20)
                                               )";

        private const string CreateMatchesTableQuery = @"CREATE TABLE IF NOT EXISTS Matches (
                                               ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                               Player1 INTEGER REFERENCES Players(ID),
                                               Player2 INTEGER REFERENCES Players(ID),
                                               Winner INTEGER REFERENCES Players(ID)
                                               );";

        private string DatabaseFile = "database.db";
        public string DatabaseSource = "data source = database.db";

        public AppDBContext()
        {
            Initialize();
        }
        public void Initialize()
        {
            //if (!File.Exists(DatabaseFile))
            //{
            //    SQLiteConnection.CreateFile(DatabaseFile);
            //}

            // Connect to the database 
            using (var connection = new SQLiteConnection(DatabaseSource))
            {
                // Create a database command
                using (var command = new SQLiteCommand(connection))
                {
                    connection.Open();

                    // Create the table
                    command.CommandText = CreatePlayersTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = CreateMatchesTableQuery;
                    command.ExecuteNonQuery();

                    connection.Close(); // Close the connection to the database
                }
            }
        }
    }
}
