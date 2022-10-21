using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X6HXFE_HFT_2021222.Models;
using X6HXFE_HFT_2021222.Models.Non_CRUD_Classes;

namespace X6HXFE_HFT_2021222.Logic.Interfaces
{
    public interface IPlayerLogic
    {
        void Create(Player item);
        void Delete(int id);
        Player Read(int id);
        IQueryable<Player> ReadAll();
        void Update(Player item);
        IEnumerable<PlayerStats> GetYoungestPlayer();
        IEnumerable<SpecificTeamByName> GetSpecificTeamsByName();
        IEnumerable<TeamStatsByPlayers> GetTeamStatsByPlayers();        
        IEnumerable<PlayerStats> GetFrenchPlayersInLigue1();        

    }
}
