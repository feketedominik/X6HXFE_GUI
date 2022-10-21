using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using X6HXFE_HFT_2021222.Logic;
using X6HXFE_HFT_2021222.Logic.Classes;
using X6HXFE_HFT_2021222.Models;
using X6HXFE_HFT_2021222.Models.Non_CRUD_Classes;
using X6HXFE_HFT_2021222.Repository.Interfaces;

namespace X6HXFE_HFT_2021222.Test
{
    [TestFixture]
    public class Tester
    {
        TeamLogic teamLogic;
        PlayerLogic playerLogic;
        LeagueLogic leagueLogic;

        Mock<IRepository<Player>> mockPlayerRepo;
        Mock<IRepository<Team>> mockTeamRepo;
        Mock<IRepository<League>> mockLeagueRepo;

        [SetUp]
        public void Init()
        {
            // Init mock repok
            mockPlayerRepo = new Mock<IRepository<Player>>();
            mockTeamRepo = new Mock<IRepository<Team>>();
            mockLeagueRepo = new Mock<IRepository<League>>();

            var leagues = new List<League>()
            {
                new League("1#Bundesliga"),
                new League("2#Ligue 1")
            }.AsQueryable();

            var teams = new List<Team>()
            {                
                new Team()
                {
                    TeamId=1,
                    LeagueId=1,
                    Name="FC Bayern Munich",
                    Founded=1900,
                    Stadium="Allianz Arena",
                    headCoach="Julian Nagelsmann",
                    League = leagues.ElementAt(0)
                },
                new Team()
                {
                    TeamId=2,
                    LeagueId=1,
                    Name="RB Leipzig",
                    Founded=2009,
                    Stadium="Red Bull Arena",
                    headCoach="Domenico Tedesco",
                    League = leagues.ElementAt(0)
                },    
                new Team()
                {
                    TeamId=3,
                    LeagueId=2,
                    Name="Paris Saint-Germain",
                    Founded=1970,
                    Stadium="Parc des Princes",
                    headCoach="Mauricio Pochettino",
                    League = leagues.ElementAt(1)
                },
                new Team()
                {
                    TeamId=4,
                    LeagueId=2,
                    Name="Lyon",
                    Founded=1950,
                    Stadium="Groupama Stadium",
                    headCoach="Peter Bosz",
                    League = leagues.ElementAt(1)
                }                
            }.AsQueryable();

            var players = new List<Player>()
            {   //Bayern
                new Player() { PlayerId = 1, TeamId = 1, Name = "Manuel Neuer", Born = DateTime.Parse("1986-03-27"), Nationality = "German", Position = "Goalkeeper", Team = teams.ElementAt(0) },
                new Player() { PlayerId = 2, TeamId = 1, Name = "Robert Lewandowski", Born = DateTime.Parse("1988-08-21"), Nationality = "Polish", Position = "Striker", Team = teams.ElementAt(0) },
                new Player() { PlayerId = 3, TeamId = 1, Name = "Thomas Müller", Born = DateTime.Parse("1989-09-13"), Nationality = "German", Position = "Attacking midfielder", Team = teams.ElementAt(0) },
                //Leipzig
                new Player() { PlayerId = 4, TeamId = 2, Name = "Peter Gulácsi", Born = DateTime.Parse("1990-05-06"), Nationality = "Hungarian", Position = "Goalkeeper", Team = teams.ElementAt(1) },                
                new Player() { PlayerId = 5, TeamId = 2, Name = "Christopher Nkunku", Born = DateTime.Parse("1997-11-14"), Nationality = "French", Position = "Midfielder", Team = teams.ElementAt(1) },
                new Player() { PlayerId = 6, TeamId = 2, Name = "Dominik Szoboszlai", Born = DateTime.Parse("2000-10-25"), Nationality = "Hungarian", Position = "Midfielder", Team = teams.ElementAt(1) },
                //PSG
                new Player() { PlayerId = 7, TeamId = 3, Name = "Kylian Mbappé", Born = DateTime.Parse("1998-12-20"), Nationality = "French", Position = "Forward", Team = teams.ElementAt(2) },
                new Player() { PlayerId = 8, TeamId = 3, Name = "Neymar", Born = DateTime.Parse("1992-02-05"), Nationality = "Brazilian", Position = "Forward", Team = teams.ElementAt(2) },
                //Lyon
                new Player() { PlayerId = 9, TeamId = 4, Name = "Tanguy Ndombele", Born = DateTime.Parse("1996-12-28"), Nationality = "French", Position = "Midfielder", Team = teams.ElementAt(3) },
                new Player() { PlayerId = 10, TeamId = 4, Name = "Moussa Dembélé", Born = DateTime.Parse("1996-07-12"), Nationality = "French", Position = "Striker", Team = teams.ElementAt(3) },
            }.AsQueryable();

            //Kapcsolatok kialakítása

            //Bayern
            teams.ElementAt(0).Players.Add(players.ElementAt(0));
            teams.ElementAt(0).Players.Add(players.ElementAt(1));
            teams.ElementAt(0).Players.Add(players.ElementAt(2));

            //Leipzig
            teams.ElementAt(1).Players.Add(players.ElementAt(3));
            teams.ElementAt(1).Players.Add(players.ElementAt(4));
            teams.ElementAt(1).Players.Add(players.ElementAt(5));
            //PSG
            teams.ElementAt(2).Players.Add(players.ElementAt(6));
            teams.ElementAt(2).Players.Add(players.ElementAt(7));
            //Lyon
            teams.ElementAt(3).Players.Add(players.ElementAt(8));
            teams.ElementAt(3).Players.Add(players.ElementAt(9));

            //Bundesliga
            leagues.ElementAt(0).Teams.Add(teams.ElementAt(0)); // Bayern
            leagues.ElementAt(0).Teams.Add(teams.ElementAt(1)); //Leipzig

            //Ligue 1
            leagues.ElementAt(1).Teams.Add(teams.ElementAt(2)); //PSG
            leagues.ElementAt(1).Teams.Add(teams.ElementAt(3)); //Lyon


            playerLogic = new PlayerLogic(mockPlayerRepo.Object);
            teamLogic = new TeamLogic(mockTeamRepo.Object);
            leagueLogic = new LeagueLogic(mockLeagueRepo.Object);

            //Setup repok
            mockTeamRepo.Setup(x => x.ReadAll()).Returns(teams);
            mockPlayerRepo.Setup(x => x.ReadAll()).Returns(players);
            mockLeagueRepo.Setup(x => x.ReadAll()).Returns(leagues);            
        }

        // 2 other test

        [Test]
        public void ReadAllPlayersTest()
        {
            var actual = playerLogic.ReadAll();
            Assert.That(mockPlayerRepo.Object.ReadAll().Count, Is.EqualTo(10));            
        }
        
        [Test]
        public void DeletePlayerTestByName()
        {
            int id = playerLogic.ReadAll().Where(x => x.Name == "Thomas Müller").Select(x => x.PlayerId).FirstOrDefault();            
            
            playerLogic.Delete(id);

            mockPlayerRepo.Verify(r => r.Delete(id), Times.Once);
            
        }

        [Test]
        public void UpdateTeamTestWithCorrectName()
        {
            var team = teamLogic.ReadAll().Where(x => x.Name == "Paris Saint-Germain").FirstOrDefault();
            team.Name = "PSG";
            //team.Founded = 2030;


            teamLogic.Update(team);

            mockTeamRepo.Verify(r => r.Update(team), Times.Once);
        }

        // 3 create test

        [Test]
        public void CreateLeagueTestWithCorrectTitle()
        {
            var league = new League()
            {
                Name = "La Liga",
            };

            leagueLogic.Create(league);

            mockLeagueRepo.Verify(r => r.Create(league), Times.Once);
        }

        [Test]
        public void CreateTeamTestWithCorrectTitleAndFoundedDate()
        {
            var team = new Team()
            {
                Name = "Borussia Dortmund",
                Founded = 1909
            };

            teamLogic.Create(team);
            
            mockTeamRepo.Verify(r => r.Create(team), Times.Once);
        }
        
        [Test]
        public void CreatePlayerTestWithInCorrectName()
        {
            var player = new Player()
            {
                Name = "CR7"
            };

            try
            {
                playerLogic.Create(player);
            }
            catch
            {
                
            }           
            mockPlayerRepo.Verify(r => r.Create(player), Times.Never);
        }


        // 5 non-crud test

        [Test]
        public void YoungestPlayerTester()
        {
            var result = playerLogic.GetYoungestPlayer().ToList();
            var expected = new List<PlayerStats>()
            {
                new PlayerStats()
                {
                    Name="Dominik Szoboszlai",
                    Born=new DateTime(2000,10,25),
                    Nationality="Hungarian",
                    Team="RB Leipzig",
                    TeamLeague="Bundesliga"
                }
            };

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void OldestTeamTester()
        {
            var result = teamLogic.GetOldestTeam().ToList();            
            var expected = new List<OldestTeamStats>()
            {
                new OldestTeamStats()
                {
                    Year = 1900,
                    OldTeam = "FC Bayern Munich",
                    OldTeamLeague = "Bundesliga"
                }
            };            
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void SpecificTeamsByNameTester()
        {
            var result = playerLogic.GetSpecificTeamsByName().ToList();
            var expected = new List<SpecificTeamByName>()
            {
                new SpecificTeamByName()
                {
                    Name = "Peter Gulácsi",
                    Position = "Goalkeeper"
                },
                new SpecificTeamByName()
                {
                    Name ="Dominik Szoboszlai",
                    Position ="Midfielder"
                }                
            };            
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void TeamStatsByPlayersTester()
        {
            var result = playerLogic.GetTeamStatsByPlayers();
            var expected = new List<TeamStatsByPlayers>()
            {
                new TeamStatsByPlayers()
                {
                    TeamName ="Lyon",
                    AVGAge = 26,
                    PlayerCount = 2
                },
                new TeamStatsByPlayers()
                {
                    TeamName ="RB Leipzig",
                    AVGAge = 26.3,
                    PlayerCount = 3
                },
                new TeamStatsByPlayers()
                {
                    TeamName ="Paris Saint-Germain",
                    AVGAge = 27,
                    PlayerCount = 2
                },
                new TeamStatsByPlayers()
                {
                    TeamName = "FC Bayern Munich",
                    AVGAge = 34.3,
                    PlayerCount = 3
                }
            };            
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void FrenchPlayersInLigue1Tester()
        {
            var result = playerLogic.GetFrenchPlayersInLigue1().ToList();
            var expected = new List<PlayerStats>()
            {
                new PlayerStats()
                {
                    Name = "Kylian Mbappé", Born = DateTime.Parse("1998-12-20"), Nationality = "French", Team = "Paris Saint-Germain", TeamLeague = "Ligue 1"
                },
                new PlayerStats()
                {
                    Name = "Tanguy Ndombele", Born = DateTime.Parse("1996-12-28"), Nationality = "French", Team = "Lyon", TeamLeague="Ligue 1"
                },
                new PlayerStats()
                {
                    Name = "Moussa Dembélé", Born = DateTime.Parse("1996-07-12"), Nationality = "French", Team = "Lyon", TeamLeague = "Ligue 1"
                }
            };            
            Assert.AreEqual(result, expected);
        }
    }
}
