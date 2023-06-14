    using MySql.Data.MySqlClient;
using ParserAngleSharp.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ParserAngleSharp
{
    class Database
    {
        readonly MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=boardgames");
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

        public void SaveGameCategory(string category_name, int game_id)
        {
            var select_category_id = new MySqlCommand("SELECT * FROM `categories` WHERE `name` = @CategoryName;", GetConnection());
            select_category_id.Parameters.Add("@CategoryName", MySqlDbType.Text).Value = category_name;

            var reader = select_category_id.ExecuteReader();
            reader.Read();
            var category_id = reader[0].ToString();
            reader.Close();

            var insert_category = new MySqlCommand("INSERT INTO `games_categories` (`game_id`, `category_id`) VALUES (@GameId, @CategoryId)", GetConnection());
            insert_category.Parameters.Add("@GameId", MySqlDbType.Int32).Value = game_id;
            insert_category.Parameters.Add("@CategoryId", MySqlDbType.Int32).Value = category_id;
            insert_category.ExecuteNonQuery();
        }
        public void Save(List<BoardGame> games)
        {
            foreach (var game in games)
            {
                SaveImage(game);
                game.Image = ComputeSHA256Hash(game.Image) + ".png";

                OpenConnection();



                var command = new MySqlCommand("INSERT INTO `games` (`id`, `name`, `image`, `description`, `play_time`, `players_number_min`, `players_number_max`, `price`, `age`) " +
                    "VALUES (NULL, @Name, @Image, @Description, @PlayTime, @PlayersNumberMin, @PlayersNumberMax, @Price, @Age); SELECT LAST_INSERT_ID();", GetConnection());

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


                int game_id = Convert.ToInt32(command.ExecuteScalar());

                if (game.Age == 18)
                {
                    SaveGameCategory("Игры для взрослых", game_id);
                }

                if (game.Age <= 8)
                {
                    SaveGameCategory("Игры для детей", game_id);
                }

                if (game.PlayersNumberMin == 2)
                {
                    SaveGameCategory("Игры для двоих", game_id);
                }

                if (game.PlayersNumberMin > 2 || (game.PlayersNumberMax != null && game.PlayersNumberMax > 2))
                {
                    SaveGameCategory("Игры для компании", game_id);
                }

                CloseConnection();

            }
        }

        public static string ComputeSHA256Hash(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }
        }



        public void SaveImage(BoardGame boardGame)
        {
            string imageUrl = boardGame.Image;

            string savePath = "C:\\OpenServer\\domains\\BoardGames\\images\\" + ComputeSHA256Hash(boardGame.Name) + ".png";

            WebClient client = new WebClient();
            client.DownloadFile(imageUrl, savePath);
        }

    }
}
