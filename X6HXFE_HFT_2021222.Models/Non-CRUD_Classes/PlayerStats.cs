using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X6HXFE_HFT_2021222.Models.Non_CRUD_Classes
{
    public class PlayerStats
    {
        public string Name { get; set; }
        public DateTime Born { get; set; }
        public string Nationality { get; set; }
        public string Team { get; set; }
        public string TeamLeague { get; set; }

        public override bool Equals(object obj)
        {
            PlayerStats b = obj as PlayerStats;

            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Name == b.Name && this.Born == b.Born && this.Nationality == b.Nationality
                    && this.Team == b.Team && this.TeamLeague == b.TeamLeague;
            }

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Born, this.Nationality, this.Team, this.TeamLeague);
        }
    }
}
