using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Data.Models;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.Services.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FootballStatistics.Services.Tests.Services
{
    public class MatchServiceTests
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnAllMatches()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.Add(new League
            {
                Id = 1,
                Name = "Premier League",
                Country = "England"
            });

            dbContext.Teams.AddRange(
                new Team
                {
                    Id = 1,
                    Name = "Arsenal",
                    LeagueId = 1,
                    StadiumId = null,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                },
                new Team
                {
                    Id = 2,
                    Name = "Manchester City",
                    LeagueId = 1,
                    StadiumId = null,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                }
            );

            dbContext.Matches.AddRange(
                new Match
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    HomeGoals = 2,
                    AwayGoals = 1,
                    MatchDate = new DateTime(2026, 1, 10)
                },
                new Match
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    HomeGoals = 0,
                    AwayGoals = 0,
                    MatchDate = new DateTime(2026, 2, 10)
                }
            );

            await dbContext.SaveChangesAsync();

            var service = new MatchService(dbContext);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnCorrectMatchDetails()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.Add(new League
            {
                Id = 1,
                Name = "Serie A",
                Country = "Italy"
            });

            dbContext.Stadiums.Add(new Stadium
            {
                Id = 1,
                Name = "Allianz Stadium",
                City = "Turin",
                Capacity = 41507
            });

            dbContext.Teams.AddRange(
                new Team
                {
                    Id = 1,
                    Name = "Juventus",
                    LeagueId = 1,
                    StadiumId = 1,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                },
                new Team
                {
                    Id = 2,
                    Name = "Napoli",
                    LeagueId = 1,
                    StadiumId = null,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                }
            );

            dbContext.Matches.Add(new Match
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeGoals = 2,
                AwayGoals = 0,
                MatchDate = new DateTime(2026, 3, 15)
            });

            await dbContext.SaveChangesAsync();

            var service = new MatchService(dbContext);

            var result = await service.GetDetailsAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Juventus", result!.HomeTeamName);
            Assert.Equal("Napoli", result.AwayTeamName);
            Assert.Equal(2, result.HomeGoals);
            Assert.Equal(0, result.AwayGoals);
            Assert.Equal("Allianz Stadium", result.Stadium);
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnNullWhenMatchDoesNotExist()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            var service = new MatchService(dbContext);

            var result = await service.GetDetailsAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWhenMatchDoesNotExist()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            var service = new MatchService(dbContext);

            var result = await service.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteMatchWhenMatchExists()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.Add(new League
            {
                Id = 1,
                Name = "La Liga",
                Country = "Spain"
            });

            dbContext.Teams.AddRange(
                new Team
                {
                    Id = 1,
                    Name = "Barcelona",
                    LeagueId = 1,
                    StadiumId = null,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                },
                new Team
                {
                    Id = 2,
                    Name = "Atletico Madrid",
                    LeagueId = 1,
                    StadiumId = null,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                }
            );

            dbContext.Matches.Add(new Match
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeGoals = 1,
                AwayGoals = 1,
                MatchDate = new DateTime(2026, 2, 20)
            });

            await dbContext.SaveChangesAsync();

            var service = new MatchService(dbContext);

            var result = await service.DeleteAsync(1);

            Assert.True(result);
            Assert.Empty(dbContext.Matches);
        }
    }
}
