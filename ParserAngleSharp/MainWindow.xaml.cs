using ParserAngleSharp.Core;
using ParserAngleSharp.Core.Colapsar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ParserAngleSharp
{
    public partial class MainWindow : Window
    {
        ParserWorker<Present> parser;
        public MainWindow()
        {
            InitializeComponent();
            parser = new ParserWorker<Present> (new ColapsarParser());
            parser.OnNewData += Parser_OnNewData;
        }


        private void Parser_OnNewData(object arg1, List<Present> arg2)
        {
            foreach (var item in arg2)
            {
                if (item.Description.Contains("\n"))
                {
                    item.Description.Replace("\n", "");

                }
                ResultList.Items.Add(item.Name + "\n" + item.Image + "\n" + item.Description + "\n"  + item.Price + "\n");
            }

            string file_name = "C:\\Users\\HP\\source\\repos\\ParserAngleSharp\\ParserAngleSharp\\Core\\Colapsar\\Presents.csv";
            var presents = arg2;
            var streamWriter = new StreamWriter(file_name, true, Encoding.UTF8);
            foreach (var present in presents)
            {
                streamWriter.WriteLine(present.Name + "\n" + present.Image + "\n" + present.Description + "\n" + present.Price + "\n");
            }
            streamWriter.Close();
            MessageBox.Show("All works done!");
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new ColapsarSettings(Int32.Parse(StartPageNumber.Text), Int32.Parse(StopPageNumber.Text));
            parser.Start();
        }
    }
}
