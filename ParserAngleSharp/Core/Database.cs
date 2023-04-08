using MySql.Data.MySqlClient;
using ParserAngleSharp.Core;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;

namespace ParserAngleSharp
{
    class Database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=boardgames");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed) 
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void Save(List<BoardGame> games)
        {
            foreach (var game in games)
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO `games` (`id`, `name`, `image`, `description`, `play_time`, `players_number_min`, `players_number_max`, `price`, `age`, `category_id`) " +
                    "VALUES (NULL, @Name, @Image, @Description, @PlayTime, @PlayersNumberMin, @PlayersNumberMax, @Price, @Age, NULL);", GetConnection());

                command.Parameters.Add("@Name", MySqlDbType.Text).Value = game.Name;
                command.Parameters.Add("@Image", MySqlDbType.Text).Value = game.Image;
                command.Parameters.Add("@Description", MySqlDbType.Text).Value = game.Description;
                command.Parameters.Add("@PlayTime", MySqlDbType.Text).Value = game.PlayTime;
                command.Parameters.Add("@PlayersNumberMin", MySqlDbType.Text).Value = game.PlayersNumberMin;

                if (game.PlayersNumberMax == 0)
                {
                    command.Parameters.Add("@PlayersNumberMax", MySqlDbType.Text).Value = null;
                }
                else
                {
                    command.Parameters.Add("@PlayersNumberMax", MySqlDbType.Text).Value = game.PlayersNumberMax;
                }

                command.Parameters.Add("@Price", MySqlDbType.Int32).Value = game.Price;
                command.Parameters.Add("@Age", MySqlDbType.Int32).Value = game.Age;

                OpenConnection();

                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Error");
                }

                CloseConnection();
            }
        }

    }
}
