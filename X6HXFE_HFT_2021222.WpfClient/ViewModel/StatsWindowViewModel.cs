using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using X6HXFE_HFT_2021222.Models.Non_CRUD_Classes;

namespace X6HXFE_HFT_2021222.WpfClient.ViewModel
{
    public class StatsWindowViewModel : ObservableRecipient
    {
        public RestCollection<SpecificTeamByName> GetSpecificTeamsByName { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public StatsWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                GetSpecificTeamsByName = new RestCollection<SpecificTeamByName>("http://localhost:8739/", "stat/GetSpecificTeamsByName");
            }
        }
    }
}
