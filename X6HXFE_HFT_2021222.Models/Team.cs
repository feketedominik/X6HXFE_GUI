using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace X6HXFE_HFT_2021222.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        public int LeagueId { get; set; }
        
        [Required]
        [StringLength(240)]
        public string Name { get; set; }
        public int Founded { get; set; }

        [StringLength(240)]
        public string Stadium { get; set; }

        [StringLength(240)]
        public string headCoach { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        [JsonIgnore]
        public virtual League League { get; set; }

        public Team()
        {
            Players = new HashSet<Player>();
        }

        public Team(string line)
        {
            string[] split = line.Split('#');
            TeamId = int.Parse(split[0]);
            LeagueId = int.Parse(split[1]);
            Name = split[2];
            Founded = int.Parse(split[3]);
            Stadium = split[4];
            headCoach = split[5];
            Players = new HashSet<Player>();
        }
    }
}
