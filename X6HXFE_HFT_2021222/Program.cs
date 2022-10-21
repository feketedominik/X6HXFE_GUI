using ConsoleTools;
using System;
using System.Collections.Generic;
using X6HXFE_HFT_2021222.Client;
using X6HXFE_HFT_2021222.Models;
using X6HXFE_HFT_2021222.Models.Non_CRUD_Classes;

namespace X6HXFE_HFT_2021222
{
    public class Program
    {
        static RestService rest;
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:8739/", "team");

            var leagueSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("League"))
                .Add("Create", () => Create("League"))
                .Add("Delete", () => Delete("League"))
                .Add("Update", () => Update("League"))
                .Add("Exit", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Exit", ConsoleMenu.Close);

            var statSubMenu = new ConsoleMenu(args, level: 1)
                .Add("YoungestPlayer", () => YoungestPlayerStat())
                .Add("OldestTeam", () => OldestTeam())
                .Add("RBLeipzigHungarianPlayers", () => SpecificTeamsByName())
                .Add("TeamStats", () => TeamStatsByPlayers())
                .Add("FrenchPlayerInLigue1", () => FrenchPlayerInLigue1())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("League", () => leagueSubMenu.Show())
                .Add("Team", () => teamSubMenu.Show())
                .Add("Player", () => playerSubMenu.Show())
                .Add("Stats", () => statSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        static void YoungestPlayerStat()
        {
            var player = rest.Get<PlayerStats>("stat/getyoungestplayer");
            Console.WriteLine("Name\tLeague\tTeam\tBorn\tNationality");
            foreach (var item in player)
            {
                Console.WriteLine($"{item.Name}\t{item.TeamLeague}\t{item.Team}\t{item.Born}\t{item.Nationality}");
            }
            Console.ReadLine();
        }

        static void OldestTeam()
        {
            var club = rest.Get<OldestTeamStats>("stat/getoldestteam");
            Console.WriteLine($"Founded\tTeam\tLeague");
            foreach (var item in club)
            {
                Console.WriteLine($"{item.Year}\t{item.OldTeam}\t{item.OldTeamLeague}");
            }
            Console.ReadLine();
        }

        static void SpecificTeamsByName()
        {
            var players = rest.Get<SpecificTeamByName>("stat/getspecificteamsbyname");
            foreach (var item in players)
            {
                Console.WriteLine($"{item.Name}\t{item.Position}");
            }
            Console.ReadLine();
        }

        static void TeamStatsByPlayers()
        {
            var stats = rest.Get<TeamStatsByPlayers>("stat/getteamstatsbyplayers");
            Console.WriteLine($"Club\tAvgAge\tPlayers");
            foreach (var item in stats)
            {
                Console.WriteLine($"{item.TeamName}\t{item.AVGAge}\t{item.PlayerCount}");
            }
            Console.ReadLine();
        }

        static void FrenchPlayerInLigue1()
        {
            var players = rest.Get<PlayerStats>("stat/getfrenchplayersinligue1");
            Console.WriteLine($"Name\tTeam");
            foreach (var item in players)
            {
                Console.WriteLine($"{item.Name}\t{item.Team}");
            }
            Console.ReadLine();
        }

        static void Update(string entity)
        {
            string answer = "";
            bool update = false;

            if (entity == "League")
            {
                Console.WriteLine("Enter League's id to update: ");
                int id = int.Parse(Console.ReadLine());
                League one = rest.Get<League>(id, "league");
                Console.WriteLine($"New league name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "league");
            }
            else if (entity == "Team")
            {                
                Console.WriteLine("Enter Team's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Team one = rest.Get<Team>(id, "team");
                Console.WriteLine($"Do you want to change the name? [old: {one.Name}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New team name [old: {one.Name}]: ");
                    string name = Console.ReadLine();
                    one.Name = name;
                }                
                Console.WriteLine($"Do you want to change the founded year? [old: {one.Founded}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New team founded [old: {one.Founded}]: ");
                    int foundedYear = int.Parse(Console.ReadLine());
                    one.Founded = foundedYear;
                }
                Console.WriteLine($"Do you want to change the stadium name? [old: {one.Stadium}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New stadium name [old: {one.Stadium}]: ");
                    string stadiumName = Console.ReadLine();
                    one.Stadium = stadiumName;
                }
                Console.WriteLine($"Do you want to change the head coach name? [old: {one.headCoach}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New head coach name [old: {one.headCoach}]: ");
                    string headCoachName = Console.ReadLine();
                    one.headCoach = headCoachName;
                }


                rest.Put(one, "team");
            }
            else if (entity == "Player")
            {
                Console.WriteLine("Enter Player's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Player one = rest.Get<Player>(id, "player");
                Console.WriteLine($"Do you want to change the name? [old: {one.Name}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New player name [old: {one.Name}]: ");
                    string playerName = Console.ReadLine();
                    one.Name = playerName;
                }
                Console.WriteLine($"Do you want to change the team id? [old: {one.TeamId}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New player team id [old: {one.TeamId}]: ");
                    int teamId = int.Parse(Console.ReadLine());
                    one.TeamId = teamId;
                }
                Console.WriteLine($"Do you want to change the born date? [old: {one.Born}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New player born date [old: {one.Born}]: ");
                    DateTime born = DateTime.Parse(Console.ReadLine());
                    one.Born = born;
                }
                Console.WriteLine($"Do you want to change the nationality? [old: {one.Nationality}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New player nationality [old: {one.Nationality}]: ");
                    string nationality = Console.ReadLine();
                    one.Nationality = nationality;
                }
                Console.WriteLine($"Do you want to change the position? [old: {one.Position}] - [y/n]");
                answer = Console.ReadLine();
                update = answer == "y" ? true : false;
                if (update)
                {
                    Console.WriteLine($"New player position [old: {one.Position}]: ");
                    string pos = Console.ReadLine();
                    one.Position = pos;
                }

                
                rest.Put(one, "player");
            }
        }

        static void Delete(string entity)
        {
            if (entity == "League")
            {
                Console.WriteLine("Enter League's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "league");
            }
            else if (entity == "Team")
            {
                Console.WriteLine("Enter Team's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "team");
            }
            else if (entity == "Player")
            {
                Console.WriteLine("Enter Player's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "player");
            }
        }

        static void Create(string entity)
        {
            if (entity == "League")
            {
                Console.WriteLine("Enter League Name: ");
                string name = Console.ReadLine();
                rest.Post(new League() { Name = name }, "league");
            }
            else if (entity == "Team")
            {
                Console.WriteLine("*Enter Team Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("*Enter League id: ");
                int leagueId = int.Parse(Console.ReadLine());
                Console.WriteLine("*Enter Founded year: ");
                int founded = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Stadium name: ");
                string stadiumName = Console.ReadLine();
                Console.WriteLine("Enter head coach name: ");
                string headCoachName = Console.ReadLine();                

                rest.Post(new Team() { Name = name, LeagueId = leagueId, Founded = founded, Stadium = stadiumName, headCoach = headCoachName }, "team");
            }
            else if (entity == "Player")
            {
                Console.WriteLine("Enter Player Name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Player Team id: ");
                int team = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Player Born [yyyy-mm-dd]: ");
                DateTime born = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Player Nationality: ");
                string nationality = Console.ReadLine();
                Console.WriteLine("Enter Player Position: ");
                string pos = Console.ReadLine();

                rest.Post(new Player() { Name = name, TeamId = team, Born = born, Nationality = nationality, Position = pos }, "player");
            }
        }

        static void List(string entity)
        {
            if (entity == "League")
            {
                List<League> leagues = rest.Get<League>("league");
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in leagues)
                {
                    Console.WriteLine(item.LeagueId + "\t" + item.Name);
                }
            }
            else if (entity == "Team")
            {
                List<Team> teams = rest.Get<Team>("team");
                Console.WriteLine("Id" + "\t" + "Name" + "\t" + "Stadium" + "\t" + "HeadCoach" + "\t" + "Founded");
                foreach (var item in teams)
                {
                    Console.WriteLine(item.TeamId + "\t" + item.Name + "\t" + item.Stadium + "\t" + item.headCoach + "\t" + item.Founded);
                }
            }
            else if (entity == "Player")
            {
                List<Player> players = rest.Get<Player>("player");
                Console.WriteLine("Id" + "\t" + "Name" + "\t" + "Born" + "\t" + "Nationality" + "\t" + "Position" + "\t" + "TeamId");
                foreach (var item in players)
                {
                    Console.WriteLine(item.PlayerId + "\t" + item.Name + "\t" + item.Born + "\t" + item.Nationality + "\t" + item.Position + "\t" + item.TeamId);
                }
            }
            Console.ReadLine();
        }
    }
}
