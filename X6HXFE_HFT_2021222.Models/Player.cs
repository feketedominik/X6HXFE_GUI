using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace X6HXFE_HFT_2021222.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        [Required]
        [StringLength(240)]
        public string Name { get; set; }
        public DateTime Born { get; set; }
        
        [StringLength(240)]
        public string Nationality { get; set; }

        [StringLength(240)]
        public string Position { get; set; }

        [JsonIgnore]
        public virtual Team Team { get; set; }

        public Player()
        {

        }

        public Player(string line)
        {
            string[] split = line.Split('#');
            PlayerId = int.Parse(split[0]);
            TeamId = int.Parse(split[1]);
            Name = split[2];
            Born = DateTime.Parse(split[3]);
            Nationality = split[4];
            Position = split[5];
        }
    }
}