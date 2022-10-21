using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X6HXFE_HFT_2021222.Models.Non_CRUD_Classes
{
    public class OldestTeamStats
    {
        public int Year { get; set; }
        public string OldTeam { get; set; }
        public string OldTeamLeague { get; set; }

        public override bool Equals(object obj)
        {
            OldestTeamStats b = obj as OldestTeamStats;

            if (b == null)
            {
                return false;
            }
            else
            {
                return this.OldTeam == b.OldTeam && this.OldTeamLeague == b.OldTeamLeague && this.Year == b.Year;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.OldTeam, this.OldTeamLeague, this.Year);
        }
    }
}
