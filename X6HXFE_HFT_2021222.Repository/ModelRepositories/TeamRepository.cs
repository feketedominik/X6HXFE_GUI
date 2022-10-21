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
    public class TeamRepository : Repository<Team>, IRepository<Team>
    {
        public TeamRepository(FootballDbContext ctx) : base(ctx)
        {
            
        }
        public override Team Read(int id)
        {
            return ctx.Teams.FirstOrDefault(t => t.TeamId == id);
        }

        public override void Update(Team item)
        {
            var old = Read(item.TeamId);
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
