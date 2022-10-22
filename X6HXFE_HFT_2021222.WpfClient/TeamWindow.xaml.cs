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
using System.Windows.Shapes;
using X6HXFE_HFT_2021222.Models;

namespace X6HXFE_HFT_2021222.WpfClient
{
    /// <summary>
    /// Interaction logic for TeamWindow.xaml
    /// </summary>
    public partial class TeamWindow : Window
    {
        //RestCollection<League> Leagues;
        public TeamWindow()
        {
            InitializeComponent();
            //this.Leagues = Leagues;
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //foreach (var item in Leagues)
            //{
            //    if (item != null)
            //    {
            //        cbLeague.Items.Add(item.Name);
            //    }
            //}
        }        
    }
}
