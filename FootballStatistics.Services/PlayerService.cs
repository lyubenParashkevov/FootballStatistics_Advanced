

using FootballStatistics.Data.Infrastructure.Database;
using FootballStatistics.Data.Models;
using FootballStatistics.Services.Contracts;
using FootballStatistics.Web.ViewModels.ViewModels.Player;
using FootballStatistics.Web.ViewModels.ViewModels.Teams;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FootballStatisticsDbContext dbContext;

        public PlayerService(FootballStatisticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PlayerListItemModel>> GetAllAsync()
        {
            return await dbContext.Players
                .AsNoTracking()
                .Select(p => new PlayerListItemModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Position = p.Position,
                    GoalsScored = p.GoalsScored,
                    TeamName = p.Team.Name
                })
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task CreateAsync(PlayerFormModel model)
        {
            Player player = new Player
            {
                Name = model.Name,
                Age = model.Age,
                Position = model.Position,
                GoalsScored = model.GoalsScored,
                TeamId = model.TeamId
            };

            await dbContext.Players.AddAsync(player);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PlayerDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Players
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PlayerDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Position = p.Position,
                    GoalsScored = p.GoalsScored,
                    TeamName = p.Team.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PlayerFormModel?> GetEditModelAsync(int id)
        {
            Player? player = await dbContext.Players
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return null;
            }

            return new PlayerFormModel
            {
                Name = player.Name,
                Age = player.Age,
                Position = player.Position,
                GoalsScored = player.GoalsScored,
                TeamId = player.TeamId,
                Teams = await GetTeamsDropdownAsync()
            };
        }

        public async Task<bool> UpdateAsync(int id, PlayerFormModel model)
        {
            Player? player = await dbContext.Players
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return false;
            }

            player.Name = model.Name;
            player.Age = model.Age;
            player.Position = model.Position;
            player.GoalsScored = model.GoalsScored;
            player.TeamId = model.TeamId;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Player? player = await dbContext.Players
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return false;
            }

            dbContext.Players.Remove(player);
            await dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<IEnumerable<TeamDropdownModel>> GetTeamsDropdownAsync()
        {
            return await dbContext.Teams
                .AsNoTracking()
                .Select(t => new TeamDropdownModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }
    }
}
