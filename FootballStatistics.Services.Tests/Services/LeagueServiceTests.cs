using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Services.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FootballStatistics.Services.Tests.Services
{
    public class LeagueServiceTests
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnAllLeagues()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.AddRange(
                new League { Id = 1, Name = "Premier League", Country = "England" },
                new League { Id = 2, Name = "La Liga", Country = "Spain" }
            );

            await dbContext.SaveChangesAsync();

            var service = new LeagueService(dbContext);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnCorrectLeagueWhenLeagueExists()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Leagues.Add(
                new League { Id = 1, Name = "Serie A", Country = "Italy" }
            );

            await dbContext.SaveChangesAsync();

            var service = new LeagueService(dbContext);

            var result = await service.GetDetailsAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Serie A", result!.Name);
            Assert.Equal("Italy", result.Country);
        }

        [Fact]
        public async Task GetDetailsAsyncShouldReturnNullWhenLeagueDoesNotExist()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            var service = new LeagueService(dbContext);

            var result = await service.GetDetailsAsync(999);

            Assert.Null(result);
        }
    }
}
