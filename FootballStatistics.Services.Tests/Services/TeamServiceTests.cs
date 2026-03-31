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
    public class TeamServiceTests
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnAllTeams()
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
                    Name = "Chelsea",
                    LeagueId = 1,
                    StadiumId = null,
                    Points = 0,
                    GoalsScored = 0,
                    GoalsConceded = 0
                }
            );

            await dbContext.SaveChangesAsync();

            var service = new TeamService(dbContext);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnCorrectTeam()
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

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Juventus",
                LeagueId = 1,
                StadiumId = 1,
                Points = 10,
                GoalsScored = 12,
                GoalsConceded = 5
            });

            await dbContext.SaveChangesAsync();

            var service = new TeamService(dbContext);

            var result = await service.GetDetailsAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Juventus", result!.Name);
            Assert.Equal("Serie A", result.LeagueName);
            Assert.Equal("Allianz Stadium", result.StadiumName);
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnNullWhenTeamDoesNotExist()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            var service = new TeamService(dbContext);

            var result = await service.GetDetailsAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseWhenTeamDoesNotExist()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            var service = new TeamService(dbContext);

            var result = await service.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteTeamWhenTeamExists()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.Add(new League
            {
                Id = 1,
                Name = "La Liga",
                Country = "Spain"
            });

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Barcelona",
                LeagueId = 1,
                StadiumId = null,
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            await dbContext.SaveChangesAsync();

            var service = new TeamService(dbContext);

            var result = await service.DeleteAsync(1);

            Assert.True(result);
            Assert.Empty(dbContext.Teams);
        }
    }
}
