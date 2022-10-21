using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X6HXFE_HFT_2021222.Logic.Interfaces;
using X6HXFE_HFT_2021222.Models;
using X6HXFE_HFT_2021222.Models.Non_CRUD_Classes;
using X6HXFE_HFT_2021222.Repository.Interfaces;

namespace X6HXFE_HFT_2021222.Logic.Classes
{
    public class PlayerLogic : IPlayerLogic
    {
        IRepository<Player> playerRepo;

        public PlayerLogic(IRepository<Player> playerRepo)
        {
            this.playerRepo = playerRepo;
        }

        public void Create(Player item)
        {
            bool containNumber = item.Name.Any(char.IsDigit);
            if (item.Name.Length < 3 || containNumber)
            {
                throw new ArgumentException("invalid player name");
            }
            this.playerRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.playerRepo.Delete(id);
        }       

        public Player Read(int id)
        {
            return this.playerRepo.Read(id);
        }

        public IQueryable<Player> ReadAll()
        {
            return this.playerRepo.ReadAll();
        }

        public void Update(Player item)
        {
            bool containNumber = item.Name.Any(char.IsDigit);
            if (item.Name.Length < 3 || containNumber)
            {
                throw new ArgumentException("invalid player name");
            }
            this.playerRepo.Update(item);
        }

        //NON-CRUD


        //Listázza ki azok nevét, akik az Rb Leipzig csapatában játszanak és magyar nemzetiségű!
        public IEnumerable<SpecificTeamByName> GetSpecificTeamsByName()
        {
            return from player in this.playerRepo.ReadAll()
                   where player.Team.Name == "RB Leipzig" && player.Nationality == "Hungarian"
                   select new SpecificTeamByName
                   {
                       Name = player.Name,
                       Position = player.Position
                   };
        }

        //Melyik csapatban szerepel a legfiatalabb játékos? Írja ki a csapat nevét, ligáját, játékos nevét, születési dátumát, és a nemzetiségét.
        public IEnumerable<PlayerStats> GetYoungestPlayer()
        {
            return from y in this.playerRepo.ReadAll()
                   where y.Born.Equals(
                       (from x in this.playerRepo.ReadAll()
                        select x.Born).Max())
                   select new PlayerStats
                   {
                       Team = y.Team.Name,
                       TeamLeague = y.Team.League.Name,
                       Name = y.Name,
                       Born = y.Born,
                       Nationality = y.Nationality
                   };
        }

        //Listázza ki a csapatok játékosainak átlagos életkorát, és hogy mennyi játékos szerepel a klubban. Rendezze átlag életkor szerint növekvő sorrendbe!
        public IEnumerable<TeamStatsByPlayers> GetTeamStatsByPlayers()
        {
            return (from x in this.playerRepo.ReadAll()
                    group x by x.Team.Name into g
                    select new TeamStatsByPlayers
                    {
                        TeamName = g.Key,
                        AVGAge = Math.Round(DateTime.Now.Year - g.Average(x => x.Born.Year), 1),
                        PlayerCount = g.Select(x => x.Name).Count()
                    }).OrderBy(x => x.AVGAge);
        }

        //Listázza ki azon francia játékosok adatait, akik a francia bajnokságban játszanak.
        public IEnumerable<PlayerStats> GetFrenchPlayersInLigue1()
        {
            return from x in playerRepo.ReadAll()
                   where x.Nationality == "French" && x.Team.League.Name == "Ligue 1"
                   select new PlayerStats
                   {
                       Team = x.Team.Name,
                       TeamLeague = x.Team.League.Name,
                       Name = x.Name,
                       Born = x.Born,
                       Nationality = x.Nationality
                   };
        }        
    }
}
