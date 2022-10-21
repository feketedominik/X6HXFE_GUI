using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X6HXFE_HFT_2021222.Models;
using X6HXFE_HFT_2021222.Repository.GenericRepository;
using X6HXFE_HFT_2021222.Repository.Interfaces;

namespace X6HXFE_HFT_2021222.Repository.ModelRepositories
{
    public class LeagueRepository : Repository<League>, IRepository<League>
    {
        public LeagueRepository(FootballDbContext ctx) : base(ctx)
        {

        }
        public override League Read(int id)
        {
            return ctx.Leagues.FirstOrDefault(t => t.LeagueId == id);
        }

        public override void Update(League item)
        {
            var old = Read(item.LeagueId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }                
            }
            ctx.SaveChanges();
        }
    }
}
