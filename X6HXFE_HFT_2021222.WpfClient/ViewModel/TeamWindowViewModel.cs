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
    public class TeamWindowViewModel : ObservableRecipient
    {
        public RestCollection<Team> Teams { get; set; }
        public RestCollection<League> Leagues { get; set; }

        private Team selectedTeam;
        private League selectedLeague;
        

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {                    
                    selectedTeam = new Team()
                    {
                        TeamId = value.TeamId,
                        LeagueId = selectedLeague.LeagueId,
                        Name = value.Name,
                        headCoach = value.headCoach,
                        Stadium = value.Stadium,
                        Founded = value.Founded
                    };
                    OnPropertyChanged();
                    (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdateLeagueCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public League SelectedLeague
        {
            get { return selectedLeague; }
            set
            {
                if (value != null)
                {
                    selectedLeague = new League()
                    {
                        Name = value.Name,
                        LeagueId = value.LeagueId
                    };                   
                    OnPropertyChanged();                    
                }
            }
        }
        public ICommand CreateTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }        
        public TeamWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Teams = new RestCollection<Team>("http://localhost:8739/", "team");
                Leagues = new RestCollection<League>("http://localhost:8739/", "league");

                CreateTeamCommand = new RelayCommand(() =>
                {                    
                    Teams.Add(new Team
                    {
                        Name = SelectedTeam.Name,                            
                        LeagueId = SelectedLeague.LeagueId,
                        Founded = SelectedTeam.Founded,
                        headCoach = SelectedTeam.headCoach,
                        Stadium = SelectedTeam.Stadium
                    });
                });
                UpdateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Update(SelectedTeam);                    
                });
                DeleteTeamCommand = new RelayCommand(() =>
                {
                      Teams.Delete(SelectedTeam.TeamId);
                },
                () =>
                {
                    return SelectedTeam != null;
                }
                );
                SelectedTeam = new Team();
            }
        }
    }
}
