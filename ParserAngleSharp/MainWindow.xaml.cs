using ParserAngleSharp.Core.Colapsar;
using ParserAngleSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParserAngleSharp
{
    public partial class MainWindow : Window
    {
        ParserWorker<Present[]> parser;
        public MainWindow()
        {
            InitializeComponent();
            parser = new ParserWorker<Present[]>(new ColapsarParser());
            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }


        private void Parser_OnNewData(object arg1, Present[] arg2)
        {
            foreach (var item in arg2)
            {
                ResultList.Items.Add(item.Name + "\n" + item.Price + "\n" + item.Image + "\n");
            }
        }

        private void Parser_OnCompleted(object obj)
        {
            MessageBox.Show("All works done!");
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            parser.Settings = new ColapsarSettings(Int32.Parse(StartPageNumber.Text), Int32.Parse(StopPageNumber.Text));
            parser.Start();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            parser.Abort();
        }
    }
}
