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
            var teams = await dbContext.Teams
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .ToListAsync();

            return teams.Select(team =>
            {
                int goalsScored = team.HomeMatches.Sum(m => m.HomeGoals)
                                 + team.AwayMatches.Sum(m => m.AwayGoals);

                int goalsConceded = team.HomeMatches.Sum(m => m.AwayGoals)
                                   + team.AwayMatches.Sum(m => m.HomeGoals);

                int homePoints = team.HomeMatches.Sum(m =>
                    m.HomeGoals > m.AwayGoals ? 3 :
                    m.HomeGoals == m.AwayGoals ? 1 : 0);

                int awayPoints = team.AwayMatches.Sum(m =>
                    m.AwayGoals > m.HomeGoals ? 3 :
                    m.AwayGoals == m.HomeGoals ? 1 : 0);

                int points = homePoints + awayPoints;

                return new TeamListItemModel
                {
                    Id = team.Id,
                    Name = team.Name,
                    LeagueName = team.League.Name,
                    Points = points,
                    GoalsScored = goalsScored,
                    GoalsConceded = goalsConceded
                };
            });
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

            bool exists = await dbContext.Teams.AnyAsync(t => t.Name == model.Name && t.LeagueId == model.LeagueId);

            if (exists)
            {
                throw new InvalidOperationException("Team already exists.");
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
            var team = await dbContext.Teams
                .AsNoTracking()
                .Include(t => t.League)
                .Include(t => t.Stadium)
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return null;
            }

            int goalsScored = team.HomeMatches.Sum(m => m.HomeGoals)
                             + team.AwayMatches.Sum(m => m.AwayGoals);

            int goalsConceded = team.HomeMatches.Sum(m => m.AwayGoals)
                               + team.AwayMatches.Sum(m => m.HomeGoals);

            int homePoints = team.HomeMatches.Sum(m =>
                m.HomeGoals > m.AwayGoals ? 3 :
                m.HomeGoals == m.AwayGoals ? 1 : 0);

            int awayPoints = team.AwayMatches.Sum(m =>
                m.AwayGoals > m.HomeGoals ? 3 :
                m.AwayGoals == m.HomeGoals ? 1 : 0);

            int points = homePoints + awayPoints;

            return new TeamDetailsViewModel
            {
                Id = team.Id,
                Name = team.Name,
                LeagueName = team.League.Name,
                Points = points,
                GoalsScored = goalsScored,
                GoalsConceded = goalsConceded,
                StadiumName = team.Stadium != null ? team.Stadium.Name : null
            };
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
