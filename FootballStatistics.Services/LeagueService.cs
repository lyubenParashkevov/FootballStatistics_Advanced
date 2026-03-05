using FootballStatistics.Services.Contracts;
using FootballStatistics.Data.Infrastructure.Database;
using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.ViewModels.League;
using Microsoft.EntityFrameworkCore;
using FootballStatistics.Web.ViewModels.ViewModels.League;

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

        public async Task<LeagueStandingsModel> GetStandingsAsync(int leagueId)
        {
            var league = await dbContext.Leagues
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == leagueId);

            if (league == null)
            {
                throw new ArgumentException("League not found.");
            }

            var teams = await dbContext.Teams
                .AsNoTracking()
                .Where(t => t.LeagueId == leagueId)
                .Select(t => new { t.Id, t.Name })
                .ToListAsync();

            var teamIds = teams.Select(t => t.Id).ToList();

            var matches = await dbContext.Matches
                .AsNoTracking()
                .Where(m => teamIds.Contains(m.HomeTeamId) && teamIds.Contains(m.AwayTeamId))
                .Select(m => new { m.HomeTeamId, m.AwayTeamId, m.HomeGoals, m.AwayGoals })
                .ToListAsync();

            var table = new Dictionary<int, LeagueStandingsRowModel>();

            foreach (var t in teams)
            {
                table[t.Id] = new LeagueStandingsRowModel
                {
                    TeamId = t.Id,
                    TeamName = t.Name
                };
            }

            foreach (var m in matches)
            {
                var home = table[m.HomeTeamId];
                var away = table[m.AwayTeamId];

                home.Played++;
                away.Played++;

                home.GoalsFor += m.HomeGoals;
                home.GoalsAgainst += m.AwayGoals;

                away.GoalsFor += m.AwayGoals;
                away.GoalsAgainst += m.HomeGoals;

                if (m.HomeGoals > m.AwayGoals)
                {
                    home.Wins++;
                    home.Points += 3;

                    away.Losses++;
                }
                else if (m.HomeGoals < m.AwayGoals)
                {
                    away.Wins++;
                    away.Points += 3;

                    home.Losses++;
                }
                else
                {
                    home.Draws++;
                    home.Points += 1;

                    away.Draws++;
                    away.Points += 1;
                }
            }

            var orderedTable = table.Values
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GoalDifference)
                .ThenByDescending(x => x.GoalsFor)
                .ThenBy(x => x.TeamName)
                .ToList();

            return new LeagueStandingsModel
            {
                LeagueId = league.Id,
                LeagueName = league.Name,
                Table = orderedTable
            };
        }
    }
}
