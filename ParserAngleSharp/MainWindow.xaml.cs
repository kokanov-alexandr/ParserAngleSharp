using MySql.Data.MySqlClient;
using ParserAngleSharp.Core;
using ParserAngleSharp.Core.IgroTime;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ParserAngleSharp
{
    public partial class MainWindow : Window
    {
        ParserWorker parser;
        List<BoardGame> presents;
        public MainWindow()
        {
            InitializeComponent();
            parser = new ParserWorker (new IgroTimeParser());
            parser.OnNewData += Parser_OnNewData;
            presents = new List<BoardGame>();
        }
     

        private void Parser_OnNewData(object arg1, List<BoardGame> arg2)
        {
            presents = arg2;

            foreach (var item in presents)
            {
                ResultList.Items.Add(item.Name + "\n" + item.Description + "\n" + item.Image + "\n" + item.PlayTime);
            }


            MessageBox.Show("All work is done!");
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new IgroTimeSettings(Int32.Parse(StartPageNumber.Text), Int32.Parse(StopPageNumber.Text));
            parser.Worker();
        }


        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (presents == null)
            {
                MessageBox.Show("Files have been saved already!");
                return;
            }

            Database database = new Database();

            foreach (var present in presents)
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO `boardgames` (`id`, `name`, `image`, `description`, `play_time`, `players_number`, `price`) " +
                    "VALUES (NULL, @Name, @Image, @Description, @PlayTime, @PlayersNumber, @Price);", database.GetConnection());

                command.Parameters.Add("@Name", MySqlDbType.Text).Value = present.Name;
                command.Parameters.Add("@Image", MySqlDbType.Text).Value = present.Image;
                command.Parameters.Add("@Description", MySqlDbType.Text).Value = present.Description;
                command.Parameters.Add("@PlayTime", MySqlDbType.Text).Value = present.PlayTime;
                command.Parameters.Add("@PlayersNumber", MySqlDbType.Text).Value = present.PlayersNumber;
                command.Parameters.Add("@Price", MySqlDbType.Int32).Value = present.Price;

                database.OpenConnection();

                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Error");
                }

                database.CloseConnection();
            }
            presents = null;
            MessageBox.Show("Files have been saved!");
        }
    }
}
