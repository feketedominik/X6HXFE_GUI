using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X6HXFE_HFT_2021222.Models.Non_CRUD_Classes
{
    public class TeamStatsByPlayers
    {
        public string TeamName { get; set; }
        public double AVGAge { get; set; }
        public int PlayerCount { get; set; }

        public override bool Equals(object obj)
        {
            TeamStatsByPlayers b = obj as TeamStatsByPlayers;

            if (b == null)
            {
                return false;
            }
            else
            {
                return this.TeamName == b.TeamName && this.AVGAge == b.AVGAge && this.PlayerCount == b.PlayerCount;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TeamName, this.AVGAge, this.PlayerCount);
        }
    }
}
