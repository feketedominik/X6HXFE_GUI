using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X6HXFE_HFT_2021222.Logic.Interfaces;
using X6HXFE_HFT_2021222.Models.Non_CRUD_Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace X6HXFE_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IPlayerLogic playerLogic;
        ITeamLogic teamLogic;

        public StatController(IPlayerLogic playerLogic, ITeamLogic teamLogic)
        {
            this.playerLogic = playerLogic;
            this.teamLogic = teamLogic;
        }
        
        [HttpGet]
        public IEnumerable<PlayerStats> GetYoungestPlayer()
        {
            return this.playerLogic.GetYoungestPlayer();
        }

        [HttpGet]
        public IEnumerable<SpecificTeamByName> GetSpecificTeamsByName()
        {
            return this.playerLogic.GetSpecificTeamsByName();
        }

        [HttpGet]
        public IEnumerable<PlayerStats> GetFrenchPlayersInLigue1()
        {
            return this.playerLogic.GetFrenchPlayersInLigue1();
        }

        [HttpGet]
        public IEnumerable<TeamStatsByPlayers> GetTeamStatsByPlayers()
        {
            return this.playerLogic.GetTeamStatsByPlayers();
        }

        [HttpGet]
        public IEnumerable<OldestTeamStats> GetOldestTeam()
        {
            return this.teamLogic.GetOldestTeam();
        }
    }
}
