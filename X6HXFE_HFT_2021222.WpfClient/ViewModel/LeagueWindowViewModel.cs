using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using X6HXFE_HFT_2021222.Models;

namespace X6HXFE_HFT_2021222.WpfClient.ViewModel
{
    public class LeagueWindowViewModel : ObservableRecipient
    {
        public RestCollection<League> Leagues { get; set; }

        private League selectedLeague;

        public League SelectedLeague
        {
            get { return selectedLeague; }
            set 
            {
                if (value != null)
                {
                    selectedLeague = new League()
                    {
                        LeagueId = value.LeagueId,
                        Name = value.Name
                    };
                    OnPropertyChanged();
                    (DeleteLeagueCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateLeagueCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateLeagueCommand { get; set; }
        public ICommand UpdateLeagueCommand { get; set; }
        public ICommand DeleteLeagueCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public LeagueWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Leagues = new RestCollection<League>("http://localhost:8739/", "league");
                CreateLeagueCommand = new RelayCommand(() =>
                {
                    Leagues.Add(new League
                    {
                        Name = SelectedLeague.Name
                    });
                });
                UpdateLeagueCommand = new RelayCommand(() =>
                {
                    Leagues.Update(SelectedLeague);
                });
                DeleteLeagueCommand = new RelayCommand(() =>
                {
                    Leagues.Delete(SelectedLeague.LeagueId);
                },
                () =>
                {
                    return SelectedLeague != null;
                });
                SelectedLeague = new League();
            }
        }
    }
}
