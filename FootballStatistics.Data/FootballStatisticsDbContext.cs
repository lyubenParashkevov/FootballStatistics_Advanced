using FootballStatistics.Common;
using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Data.Models;
using FootballStatistics.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FootballStatistics.Data.Infrastructure.Database
{
    public class FootballStatisticsDbContext : IdentityDbContext
    {
        public FootballStatisticsDbContext(DbContextOptions<FootballStatisticsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<League> Leagues { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Stadium> Stadiums { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(FootballStatisticsDbContext).Assembly);

            builder.Entity<League>().HasData(
                new League { Id = 1, Name = "Premier League", Country = "England" },
                new League { Id = 2, Name = "La Liga", Country = "Spain" },
                new League { Id = 3, Name = "Serie A", Country = "Italy" }
            );

            builder.Entity<Stadium>().HasData(
                new Stadium { Id = 1, Name = "Emirates Stadium", City = "London", Capacity = 60704 },
                new Stadium { Id = 2, Name = "Etihad Stadium", City = "Manchester", Capacity = 53400 },
                new Stadium { Id = 3, Name = "Spotify Camp Nou", City = "Barcelona", Capacity = 99354 },
                new Stadium { Id = 4, Name = "Riyadh Air Metropolitano", City = "Madrid", Capacity = 70460 },
                new Stadium { Id = 5, Name = "Allianz Stadium", City = "Turin", Capacity = 41507 },
                new Stadium { Id = 6, Name = "Stadio Diego Armando Maradona", City = "Naples", Capacity = 54726 }
            );

            builder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "Arsenal",
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    LeagueId = 1,
                    StadiumId = 1
                },
                new Team
                {
                    Id = 2,
                    Name = "Manchester City",
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    LeagueId = 1,
                    StadiumId = 2
                },
                new Team
                {
                    Id = 3,
                    Name = "Barcelona",
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    LeagueId = 2,
                    StadiumId = 3
                },
                new Team
                {
                    Id = 4,
                    Name = "Atletico Madrid",
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    LeagueId = 2,
                    StadiumId = 4
                },
                new Team
                {
                    Id = 5,
                    Name = "Juventus",
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    LeagueId = 3,
                    StadiumId = 5
                },
                new Team
                {
                    Id = 6,
                    Name = "Napoli",
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0,
                    LeagueId = 3,
                    StadiumId = 6
                }
            );

            builder.Entity<Player>().HasData(
                new Player { Id = 1, Name = "David Raya", Age = 29, Position = PlayerPosition.Goalkeeper, GoalsScored = 0, TeamId = 1 },
                new Player { Id = 2, Name = "Martin Odegaard", Age = 27, Position = PlayerPosition.Midfielder, GoalsScored = 6, TeamId = 1 },
                new Player { Id = 3, Name = "Bukayo Saka", Age = 24, Position = PlayerPosition.Forward, GoalsScored = 10, TeamId = 1 },

                new Player { Id = 4, Name = "Ederson", Age = 32, Position = PlayerPosition.Goalkeeper, GoalsScored = 0, TeamId = 2 },
                new Player { Id = 5, Name = "Rodri", Age = 30, Position = PlayerPosition.Midfielder, GoalsScored = 5, TeamId = 2 },
                new Player { Id = 6, Name = "Erling Haaland", Age = 25, Position = PlayerPosition.Forward, GoalsScored = 15, TeamId = 2 },

                new Player { Id = 7, Name = "Marc-Andre ter Stegen", Age = 34, Position = PlayerPosition.Goalkeeper, GoalsScored = 0, TeamId = 3 },
                new Player { Id = 8, Name = "Pedri", Age = 23, Position = PlayerPosition.Midfielder, GoalsScored = 4, TeamId = 3 },
                new Player { Id = 9, Name = "Robert Lewandowski", Age = 37, Position = PlayerPosition.Forward, GoalsScored = 13, TeamId = 3 },

                new Player { Id = 10, Name = "Jan Oblak", Age = 33, Position = PlayerPosition.Goalkeeper, GoalsScored = 0, TeamId = 4 },
                new Player { Id = 11, Name = "Koke", Age = 34, Position = PlayerPosition.Midfielder, GoalsScored = 2, TeamId = 4 },
                new Player { Id = 12, Name = "Antoine Griezmann", Age = 35, Position = PlayerPosition.Forward, GoalsScored = 11, TeamId = 4 },

                new Player { Id = 13, Name = "Michele Di Gregorio", Age = 29, Position = PlayerPosition.Goalkeeper, GoalsScored = 0, TeamId = 5 },
                new Player { Id = 14, Name = "Manuel Locatelli", Age = 28, Position = PlayerPosition.Midfielder, GoalsScored = 3, TeamId = 5 },
                new Player { Id = 15, Name = "Dusan Vlahovic", Age = 26, Position = PlayerPosition.Forward, GoalsScored = 12, TeamId = 5 },

                new Player { Id = 16, Name = "Alex Meret", Age = 29, Position = PlayerPosition.Goalkeeper, GoalsScored = 0, TeamId = 6 },
                new Player { Id = 17, Name = "Stanislav Lobotka", Age = 31, Position = PlayerPosition.Midfielder, GoalsScored = 1, TeamId = 6 },
                new Player { Id = 18, Name = "Victor Osimhen", Age = 27, Position = PlayerPosition.Forward, GoalsScored = 14, TeamId = 6 }
            );

            builder.Entity<Match>().HasData(
                new Match
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    HomeGoals = 2,
                    AwayGoals = 2,
                    MatchDate = new DateTime(2026, 1, 12)
                },
                new Match
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    HomeGoals = 1,
                    AwayGoals = 3,
                    MatchDate = new DateTime(2026, 2, 23)
                },
                new Match
                {
                    Id = 3,
                    HomeTeamId = 3,
                    AwayTeamId = 4,
                    HomeGoals = 2,
                    AwayGoals = 1,
                    MatchDate = new DateTime(2026, 1, 18)
                },
                new Match
                {
                    Id = 4,
                    HomeTeamId = 4,
                    AwayTeamId = 3,
                    HomeGoals = 0,
                    AwayGoals = 0,
                    MatchDate = new DateTime(2026, 3, 2)
                },
                new Match
                {
                    Id = 5,
                    HomeTeamId = 5,
                    AwayTeamId = 6,
                    HomeGoals = 1,
                    AwayGoals = 1,
                    MatchDate = new DateTime(2026, 1, 25)
                },
                new Match
                {
                    Id = 6,
                    HomeTeamId = 6,
                    AwayTeamId = 5,
                    HomeGoals = 2,
                    AwayGoals = 0,
                    MatchDate = new DateTime(2026, 2, 15)
                }
            );
        }
    }
}
