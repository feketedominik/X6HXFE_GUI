using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X6HXFE_HFT_2021222.Models;

namespace X6HXFE_HFT_2021222.Logic.Interfaces
{
    public interface ILeagueLogic
    {
        void Create(League item);
        void Delete(int id);
        League Read(int id);
        IQueryable<League> ReadAll();
        void Update(League item);
    }
}
