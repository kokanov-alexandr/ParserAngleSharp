using MySql.Data.MySqlClient;
using ParserAngleSharp.Core;
using ParserAngleSharp.Core.Colapsar;
using ParserAngleSharp.Core.MrGeek;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ParserAngleSharp
{
    public partial class MainWindow : Window
    {
        ParserWorker parser;
        List<Present> presents;
        public MainWindow()
        {
            InitializeComponent();
            parser = new ParserWorker (new LeFuturParser());
            parser.OnNewData += Parser_OnNewData;
            presents = new List<Present>();
        }
        private void SaveinFile()
        {
            string file_name = "C:\\Users\\HP\\source\\repos\\ParserAngleSharp\\ParserAngleSharp\\Core\\Presents.csv";

            var streamWriter = new StreamWriter(file_name, true, Encoding.UTF8);

            foreach (var present in presents)
            {
                streamWriter.WriteLine(present.Name + "\n" + present.Image + "\n" + present.Description + "\n" + present.Price);
            }

            streamWriter.Close();
        }

        private void Parser_OnNewData(object arg1, List<Present> arg2)
        {
            presents = arg2;

            foreach (var item in presents)
            {
                ResultList.Items.Add(item.Name + "\n" + item.Image + "\n" + item.Description + "\n"  + item.Price);
            }

            SaveinFile();

            MessageBox.Show("All work is done!");
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new LeFuturSettings(Int32.Parse(StartPageNumber.Text), Int32.Parse(StopPageNumber.Text));
            parser.Start();
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
                MySqlCommand command = new MySqlCommand("INSERT INTO `products` (`id`, `name`, `description`, `image`, `price`) VALUES (NULL, @Name, @Description, @Image, @Price);", database.GetConnection());
                
                command.Parameters.Add("@Name", MySqlDbType.Text).Value = present.Name;
                command.Parameters.Add("@Description", MySqlDbType.Text).Value = present.Description;
                command.Parameters.Add("@Image", MySqlDbType.Text).Value = present.Image;
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
