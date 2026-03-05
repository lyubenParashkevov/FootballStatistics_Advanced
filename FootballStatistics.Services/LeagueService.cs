using FootballStatistics.Services.Contracts;
using FootballStatistics.Data.Infrastructure.Database;
using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.ViewModels.League;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly FootballStatisticsDbContext dbContext;

        public LeagueService(FootballStatisticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<LeagueListItemModel>> GetAllAsync()
        {
            return await dbContext.Leagues
                .AsNoTracking()
                .OrderBy(l => l.Name)
                .Select(l => new LeagueListItemModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Country = l.Country
                })
                .ToListAsync();
        }

        public async Task CreateAsync(LeagueFormModel model)
        {
            var league = new League
            {
                Name = model.Name,
                Country = model.Country
            };

            await dbContext.Leagues.AddAsync(league);
            await dbContext.SaveChangesAsync();
        }

        public async Task<LeagueDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Leagues
                .AsNoTracking()
                .Where(l => l.Id == id)
                .Select(l => new LeagueDetailsViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Country = l.Country,
                    Teams = l.Teams
                        .OrderBy(t => t.Name)
                        .Select(t => t.Name)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LeagueFormModel?> GetEditModelAsync(int id)
        {
            return await dbContext.Leagues
                .AsNoTracking()
                .Where(l => l.Id == id)
                .Select(l => new LeagueFormModel
                {
                    Name = l.Name,
                    Country = l.Country
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(int id, LeagueFormModel model)
        {
            var league = await dbContext.Leagues.FindAsync(id);
            if (league == null)
            {
                return false;
            }

            league.Name = model.Name;
            league.Country = model.Country;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var league = await dbContext.Leagues.FindAsync(id);
            if (league == null)
            {
                return false;
            }

            try
            {
                dbContext.Leagues.Remove(league);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Cannot delete league because it has team who played matches.");
            }
   
        }
    }
}
