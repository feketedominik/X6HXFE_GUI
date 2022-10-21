using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace X6HXFE_HFT_2021222.Models
{
    public class League
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeagueId { get; set; }

        [Required]
        [StringLength(240)]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public League()
        {
            Teams = new HashSet<Team>();
        }

        public League(string line)
        {
            string[] split = line.Split('#');
            LeagueId = int.Parse(split[0]);
            Name = split[1];
            Teams = new HashSet<Team>();
        }
    }
}