using MySql.Data.MySqlClient;
using ParserAngleSharp.Core;
using ParserAngleSharp.Core.IgroTime;
using ParserAngleSharp.Core.Mosigra;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ParserAngleSharp
{
    public partial class MainWindow : Window
    {
        ParserWorker parser;
        List<BoardGame> games;
        Database database;
        public MainWindow()
        {
            InitializeComponent();
            parser = new ParserWorker (new LavkaigrParser());
            parser.OnNewData += Parser_OnNewData;
            parser.OnPasedPage += Parser_OnPasedPage;
            games = new List<BoardGame>();
            database = new Database();
        }
     
        private void Parser_OnNewData(object arg1, List<BoardGame> arg2)
        {
            games = arg2;
            MessageBox.Show("All work is done!");
        }

        private void Parser_OnPasedPage(object arg1, int arg2)
        {
            ResultList.Items.Add("Page passed: " + arg2.ToString());
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new LavkaigrSettings(Int32.Parse(StartPageNumber.Text), Int32.Parse(StopPageNumber.Text));
            parser.Worker();

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (games == null)
            {
                MessageBox.Show("Files have been saved already!");
                return;
            }
            database.Save(games);
            games = null;
            MessageBox.Show("Files have been saved!");
        }
    }
}
