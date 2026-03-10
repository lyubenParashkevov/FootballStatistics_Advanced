using FootballStatistics.Services.Contracts;
using FootballStatistics.Data.Infrastructure.Database;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.ViewModels.League;
using FootballStatistics.ViewModels.Team;
using FootballStatistics.ViewModels.Teams;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Services
{
    public class TeamService : ITeamService
    {
        private readonly FootballStatisticsDbContext dbContext;

        public TeamService(FootballStatisticsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TeamListItemModel>> GetAllAsync()
        {
            return await dbContext.Teams
                .AsNoTracking()
                .Select(t => new TeamListItemModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Points = t.Points,
                    GoalsScored = t.GoalsScored,
                    GoalsConceded = t.GoalsConceded,
                    LeagueName = t.League.Name
                })
                .OrderByDescending(t => t.Points)
                .ToListAsync();
        }

        public async Task<TeamFormModel> GetCreateModelAsync()
        {
            return new TeamFormModel
            {
                Leagues = await GetLeaguesAsync()
            };
        }

        public async Task<TeamFormModel?> GetEditModelAsync(int id)
        {
            var model = await dbContext.Teams
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TeamFormModel
                {
                    Name = t.Name,
                    GoalsScored = t.GoalsScored,
                    GoalsConceded = t.GoalsConceded,
                    LeagueId = t.LeagueId
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return null;
            }

            model.Leagues = await GetLeaguesAsync();
            return model;
        }

        public async Task CreateAsync(TeamFormModel model)
        {

            if (model.LeagueId == null)
            {
                throw new InvalidOperationException("League is required.");
            }

            bool leagueExists = await dbContext.Leagues.AnyAsync(l => l.Id == model.LeagueId);

            if (!leagueExists)
            {
                throw new InvalidOperationException("Selected league does not exist.");
            }


            var team = new Team
            {
                Name = model.Name,
                GoalsScored = model.GoalsScored,
                GoalsConceded = model.GoalsConceded,
                LeagueId = model.LeagueId.Value,
                Points = 0
            };

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, TeamFormModel model)
        {
            var team = await dbContext.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }

            if (model.LeagueId == null)
            {
                throw new InvalidOperationException("League is required.");
            }

            bool leagueExists = await dbContext.Leagues.AnyAsync(l => l.Id == model.LeagueId);

            if (!leagueExists)
            {
                throw new InvalidOperationException("Selected league does not exist.");
            }

           

            team.Name = model.Name;
            team.GoalsScored = model.GoalsScored;
            team.GoalsConceded = model.GoalsConceded;
            team.LeagueId = model.LeagueId.Value;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var team = await dbContext.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }

            try
            {
                dbContext.Teams.Remove(team);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Cannot delete team because it has played matches.");
            }
        }

        public async Task<bool> ExistsAsync(int id)
            => await dbContext.Teams.AnyAsync(t => t.Id == id);

        public async Task<TeamDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Teams
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TeamDetailsViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    LeagueName = t.League.Name,
                    Points = t.Points,
                    GoalsScored = t.GoalsScored,
                    GoalsConceded = t.GoalsConceded
                })
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<LeagueDropdownModel>> GetLeaguesAsync()
        {
            return await dbContext.Leagues
                .AsNoTracking()
                .OrderBy(l => l.Name)
                .Select(l => new LeagueDropdownModel
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .ToListAsync();
        }


    }
}
