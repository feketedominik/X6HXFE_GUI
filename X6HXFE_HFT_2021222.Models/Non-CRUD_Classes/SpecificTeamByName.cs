using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X6HXFE_HFT_2021222.Models.Non_CRUD_Classes
{
    public class SpecificTeamByName
    {
        public string Name { get; set; }
        public string Position { get; set; }

        public override bool Equals(object obj)
        {
            SpecificTeamByName b = obj as SpecificTeamByName;

            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Name == b.Name && this.Position == b.Position;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Name, this.Position);
        }
    }
}
