using System;
using System.Linq;
using X6HXFE_HFT_2021222.Logic.Interfaces;
using X6HXFE_HFT_2021222.Models;
using X6HXFE_HFT_2021222.Repository.Interfaces;

namespace X6HXFE_HFT_2021222.Logic
{
    public class LeagueLogic : ILeagueLogic
    {
        IRepository<League> leagueRepo;

        public LeagueLogic(IRepository<League> leagueRepo)
        {
            this.leagueRepo = leagueRepo;
        }

        public void Create(League item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("league name too short...");
            }
            this.leagueRepo.Create(item);
        }

        public void Delete(int id)
        {
            this.leagueRepo.Delete(id);
        }

        public League Read(int id)
        {
            return this.leagueRepo.Read(id);
        }

        public IQueryable<League> ReadAll()
        {
            return this.leagueRepo.ReadAll();
        }

        public void Update(League item)
        {
            if (item.Name.Length < 3)
            {
                throw new ArgumentException("league name too short...");
            }
            this.leagueRepo.Update(item);
        }
    }
}
