using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Data.Models;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.Services.Tests.Helpers;
using FootballStatistics.Web.ViewModels.ViewModels.Stadium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FootballStatistics.Services.Tests.Services
{
    public class StadiumServiceTests
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnAllStadiums()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Stadiums.AddRange(
                new Stadium { Id = 1, Name = "Emirates Stadium", City = "London", Capacity = 60000 },
                new Stadium { Id = 2, Name = "Camp Nou", City = "Barcelona", Capacity = 90000 }
            );

            await dbContext.SaveChangesAsync();

            var service = new StadiumService(dbContext);

            var result = await service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldCreateStadiumAndAssignItToTeam()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.Add(new League
            {
                Id = 1,
                Name = "Premier League",
                Country = "England"
            });

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Arsenal",
                LeagueId = 1,
                StadiumId = null,
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            await dbContext.SaveChangesAsync();

            var service = new StadiumService(dbContext);

            var model = new StadiumFormModel
            {
                Name = "Emirates Stadium",
                City = "London",
                Capacity = 60704,
                TeamId = 1
            };

            await service.CreateAsync(model);

            var stadium = dbContext.Stadiums.FirstOrDefault();
            var team = dbContext.Teams.FirstOrDefault(t => t.Id == 1);

            Assert.NotNull(stadium);
            Assert.Equal("Emirates Stadium", stadium!.Name);

            Assert.NotNull(team);
            Assert.Equal(stadium.Id, team!.StadiumId);
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnCorrectStadium()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Stadiums.Add(new Stadium
            {
                Id = 1,
                Name = "Allianz Stadium",
                City = "Turin",
                Capacity = 41507
            });

            await dbContext.SaveChangesAsync();

            var service = new StadiumService(dbContext);

            var result = await service.GetDetailsAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Allianz Stadium", result!.Name);
            Assert.Equal("Turin", result.City);
            Assert.Equal(41507, result.Capacity);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteStadiumAndRemoveItFromTeams()
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
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            await dbContext.SaveChangesAsync();

            var service = new StadiumService(dbContext);

            var result = await service.DeleteAsync(1);

            var deletedStadium = dbContext.Stadiums.FirstOrDefault(s => s.Id == 1);
            var team = dbContext.Teams.FirstOrDefault(t => t.Id == 1);

            Assert.True(result);
            Assert.Null(deletedStadium);
            Assert.NotNull(team);
            Assert.Null(team!.StadiumId);
        }
    }
}
