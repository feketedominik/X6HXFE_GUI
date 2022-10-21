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
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(FootballDbContext ctx) : base(ctx)
        {

        }
        public override Player Read(int id)
        {
            return ctx.Players.FirstOrDefault(t => t.PlayerId == id);
        }

        public override void Update(Player item)
        {
            var old = Read(item.PlayerId);
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
