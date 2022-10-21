using Microsoft.EntityFrameworkCore;
using System;
using X6HXFE_HFT_2021222.Models;

namespace X6HXFE_HFT_2021222.Repository
{
    public class FootballDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<League> Leagues { get; set; }

        public FootballDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseLazyLoadingProxies().UseInMemoryDatabase("football");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(player => player.HasOne(player => player.Team)
            .WithMany(team => team.Players)
            .HasForeignKey(player => player.TeamId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Team>(team => team.HasOne(team => team.League)
            .WithMany(League => League.Teams)
            .HasForeignKey(team => team.LeagueId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<League>().HasData(new League[]
            {
                new League("1#Premier League"),
                new League("2#La Liga"),
                new League("3#Bundesliga"),
                new League("4#Serie A"),
                new League("5#Ligue 1")
            });

            //TeamId#LeagueId#Name#Founded#Stadium#headCoach
            modelBuilder.Entity<Team>().HasData(new Team[]
            {
                new Team("1#3#FC Bayern Munich#1900#Allianz Arena#Julian Nagelsmann"),
                new Team("2#3#RB Leipzig#2009#Red Bull Arena#Domenico Tedesco"),
                new Team("3#1#Chelsea FC#1905#Stamford Bridge#Thomas Tuchel"),
                new Team("4#1#Manchester City#1894#Etihad Stadion#Pep Guardiola"),
                new Team("5#2#Real Madrid CF#1902#Bernabéu#Carlo Ancelotti"),
                new Team("6#2#FC Barcelona#1899#Camp Nou#Xavi"),
                new Team("7#4#Juventus#1897#Juventus Stadium#Massimiliano Allegri"),
                new Team("8#4#A.C. Milan#1899#San Siro#Stefano Pioli"),
                new Team("9#5#Paris Saint-Germain#1970#Parc des Princes#Mauricio Pochettino"),
                new Team("10#5#Lyon#1950#Groupama Stadium#Peter Bosz"),
            });

            //PlayerId#TeamId#Name#Born#Nationality#Position
            modelBuilder.Entity<Player>().HasData(new Player[]
            {
                new Player("1#1#Manuel Neuer#1986-03-27#German#Goalkeeper"),
                new Player("2#1#Robert Lewandowski#1988-08-21#Polish#Striker"),
                new Player("3#1#Thomas Müller#1989-09-13#German#Attacking midfielder"),
                new Player("4#1#Dayot Upamecano#1998-10-27#French#Centre-back"),
                new Player("5#1#Joshua Kimmich#1986-03-27#German#Midfielder"),
                new Player("6#2#Peter Gulácsi#1990-05-06#Hungarian#Goalkeeper"),
                new Player("7#2#André Silva#1995-11-06#Portuguese#Striker"),
                new Player("8#2#Christopher Nkunku#1997-11-14#French#Midfielder"),
                new Player("9#2#Dominik Szoboszlai#2000-10-25#Hungarian#Midfielder"),
                new Player("10#2#Willi Orban#1992-11-03#Hungarian#Centre-back"),
                new Player("11#3#Édouard Mendy#1992-03-01#Senegalese#Goalkeeper"),
                new Player("12#3#Ben Chilwell#1996-12-21#English#Left-back"),
                new Player("13#3#N'Golo Kanté#1991-03-29#French#Central midfielder"),
                new Player("14#3#Kai Havertz#1999-06-11#German#Attacking midfielder"),
                new Player("15#3#Romelu Lukaku#1993-05-13#Belgian#Striker"),
                new Player("16#4#Ederson#1993-08-17#Brazilian#Goalkeeper"),
                new Player("17#4#Raheem Sterling#1994-12-08#English#Winger"),
                new Player("18#4#Phil Foden#2000-05-28#English#Midfielder"),
                new Player("19#4#Rúben Dias#1997-05-14#Portuguese#Centre-back"),
                new Player("20#4#Kevin De Bruyne#1991-06-28#Belgian#Midfielder"),
                new Player("21#5#Thibaut Courtois#1992-05-11#Belgian#Goalkeeper"),
                new Player("22#5#David Alaba#1992-06-24#Austrian#Centre-back"),
                new Player("23#5#Karim Benzema#1987-12-19#French#Striker"),
                new Player("24#5#Toni Kroos#1990-01-04#German#Midfielder"),
                new Player("25#5#Vinícius Júnior#2000-07-12#Brazilian#Winger"),
                new Player("26#6#Marc-André ter Stegen#1992-04-30#German#Goalkeeper"),
                new Player("27#6#Gerard Piqué#1987-02-02#Spanish#Centre-back"),
                new Player("28#6#Ferran Torres#2000-02-29#Spanish#Forward"),
                new Player("29#6#Adama Traoré#1996-01-25#Spanish#Winger"),
                new Player("30#6#Memphis Depay#1994-02-13#Dutch#Striker"),
                new Player("31#7#Wojciech Szczęsny#1990-04-18#Polish#Goalkeeper"),
                new Player("32#7#Giorgio Chiellini#1984-08-14#Italian#Centre-back"),
                new Player("33#7#Paulo Dybala#1993-11-15#Argentine#Forward"),
                new Player("34#7#Manuel Locatelli#1998-01-08#Italian#Midfielder"),
                new Player("35#7#Matthijs de Ligt#1999-08-12#Dutch#Centre-back"),
                new Player("36#8#Mike Maignan#1995-07-03#French#Goalkeeper"),
                new Player("37#8#Simon Kjaer#1989-03-26#Danish#Centre-back"),
                new Player("38#8#Sandro Tonali#2000-05-08#Italian#Midfielder"),
                new Player("39#8#Zlatan Ibrahimović#1981-10-03#Swedish#Striker"),
                new Player("40#8#Theo Hernandez#1997-10-06#French#Left-back"),
                new Player("41#9#Gianluigi Donnarumma#1999-02-25#Italian#Goalkeeper"),
                new Player("42#9#Sergio Ramos#1986-03-30#Spanish#Centre-back"),
                new Player("43#9#Marco Verratti#1992-11-05#Italian#Midfielder"),
                new Player("44#9#Kylian Mbappé#1998-12-20#French#Forward"),
                new Player("45#9#Neymar#1992-02-05#Brazilian#Forward"),
                new Player("46#10#Anthony Lopes#1990-10-01#Portuguese#Goalkeeper"),
                new Player("47#10#Jérôme Boateng#1988-09-03#German#Centre-back"),
                new Player("48#10#Tanguy Ndombele#1996-12-28#French#Midfielder"),
                new Player("49#10#Moussa Dembélé#1996-07-12#French#Striker"),
                new Player("50#10#Jason Denayer#1995-06-28#Belgian#Centre-back"),
            });
        }
    }
}
