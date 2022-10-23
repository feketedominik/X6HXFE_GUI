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
    public class PlayerWindowViewModel : ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }
        public RestCollection<Player> Teams { get; set; }

        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        PlayerId = value.PlayerId,
                        Name = value.Name,
                        Nationality = value.Nationality,
                        Born = value.Born,
                        Position = value.Position,
                        TeamId = value.TeamId
                    };
                    OnPropertyChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreatePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public PlayerWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Players = new RestCollection<Player>("http://localhost:8739/", "player");
                Teams = new RestCollection<Player>("http://localhost:8739/", "team");

                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.PlayerId);
                },
                () =>
                {
                    return SelectedPlayer != null;
                }
                );
                SelectedPlayer = new Player();
            }
        }
    }
}
