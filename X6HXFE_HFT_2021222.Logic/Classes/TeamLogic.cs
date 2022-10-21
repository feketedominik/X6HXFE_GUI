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
    public class TeamLogic : ITeamLogic
    {
        IRepository<Team> teamRepo;

        public TeamLogic(IRepository<Team> teamRepo)
        {
            this.teamRepo = teamRepo;
        }

        public void Create(Team item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("team name too short...");
            }
            else if (item.Founded > DateTime.Now.Year || item.Founded < 0)
            {
                throw new ArgumentException("date invalid...");
            }

            this.teamRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.teamRepo.Delete(id);
        }        

        public Team Read(int id)
        {
            var team = this.teamRepo.Read(id);
            if (team == null)
            {
                throw new ArgumentException("Team not exists");
            }
            return team;
        }

        public IQueryable<Team> ReadAll()
        {
            return this.teamRepo.ReadAll();
        }

        public void Update(Team item)
        {
            if (item.Name.Length < 3 || item.Founded > DateTime.Now.Year || item.Founded < 0)
            {
                throw new ArgumentException("invalid team date");
            }
            this.teamRepo.Update(item);
        }

        //NON-CRUD


        //Jelenítse meg a legrégebben alapított csapatot, írja ki az alapítási évet, csapat nevét, és hogy melyik ligában szerepel.
        public IEnumerable<OldestTeamStats> GetOldestTeam()
        {
            return (from x in teamRepo.ReadAll()
                    where x.Founded.Equals((from y in teamRepo.ReadAll() select y.Founded).Min())
                    select new OldestTeamStats
                    {
                        Year = x.Founded,
                        OldTeam = x.Name,
                        OldTeamLeague = x.League.Name
                    }).Distinct();
        }
    }
}
