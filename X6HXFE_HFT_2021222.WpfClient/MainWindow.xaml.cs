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
using X6HXFE_HFT_2021222.Models;

namespace X6HXFE_HFT_2021222.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RestCollection<League> Leagues { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Leagues = new RestCollection<League>("http://localhost:8739/", "league");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LeagueWindow lw = new LeagueWindow();
            lw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TeamWindow tw = new TeamWindow();
            tw.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PlayerWindow pw = new PlayerWindow();
            pw.ShowDialog();
        }
    }
}
