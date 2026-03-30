using FootballStatistics.Common;
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
    public class PlayerServiceTests
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnAllPlayersWhenNoFilters()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Test Team",
                LeagueId = 1,
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            dbContext.Players.AddRange(
                new Player { Id = 1, Name = "Player One", Age = 20, Position = PlayerPosition.Forward, GoalsScored = 5, TeamId = 1 },
                new Player { Id = 2, Name = "Player Two", Age = 22, Position = PlayerPosition.Midfielder, GoalsScored = 3, TeamId = 1 }
            );

            await dbContext.SaveChangesAsync();

            var service = new PlayerService(dbContext);

            var result = await service.GetAllAsync();

            Assert.Equal(2, result.Players.Count());
        }

        [Fact]
        public async Task GetAllAsyncShouldFilterBySearchTerm()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Test Team",
                LeagueId = 1,
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            dbContext.Players.AddRange(
                new Player { Id = 1, Name = "Messi", Age = 36, Position = PlayerPosition.Forward, GoalsScored = 10, TeamId = 1 },
                new Player { Id = 2, Name = "Ronaldo", Age = 38, Position = PlayerPosition.Forward, GoalsScored = 12, TeamId = 1 }
            );

            await dbContext.SaveChangesAsync();

            var service = new PlayerService(dbContext);

            var result = await service.GetAllAsync("Messi");

            Assert.Single(result.Players);
            Assert.Equal("Messi", result.Players.First().Name);
        }

        [Fact]
        public async Task GetAllAsyncShouldFilterByPosition()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Test Team",
                LeagueId = 1,
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            dbContext.Players.AddRange(
                new Player { Id = 1, Name = "Player A", Age = 20, Position = PlayerPosition.Forward, GoalsScored = 5, TeamId = 1 },
                new Player { Id = 2, Name = "Player B", Age = 22, Position = PlayerPosition.Defender, GoalsScored = 1, TeamId = 1 }
            );

            await dbContext.SaveChangesAsync();

            var service = new PlayerService(dbContext);

            var result = await service.GetAllAsync(null, PlayerPosition.Defender);

            Assert.Single(result.Players);
            Assert.Equal(PlayerPosition.Defender.ToString(), result.Players.First().Position);
        }

        [Fact]
        public async Task GetAllAsyncShouldApplyPagination()
        {
            using var dbContext = DbContextHelper.CreateInMemoryDbContext();

            dbContext.Teams.Add(new Team
            {
                Id = 1,
                Name = "Test Team",
                LeagueId = 1,
                Points = 0,
                GoalsScored = 0,
                GoalsConceded = 0
            });

            for (int i = 1; i <= 10; i++)
            {
                dbContext.Players.Add(new Player
                {
                    Id = i,
                    Name = $"Player {i}",
                    Age = 20,
                    Position = PlayerPosition.Forward,
                    GoalsScored = i,
                    TeamId = 1
                });
            }

            await dbContext.SaveChangesAsync();

            var service = new PlayerService(dbContext);

            var result = await service.GetAllAsync(null, null, page: 2, pageSize: 5);

            Assert.Equal(5, result.Players.Count());
            Assert.Equal(2, result.CurrentPage);
        }
    }
}
