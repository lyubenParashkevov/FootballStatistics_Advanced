using FootballStatistics.Data.Infrastructure.Database;
using FootballStatistics.Data.Models;
using FootballStatistics.Infrastructure.Models;
using FootballStatistics.Services.Contracts;
using FootballStatistics.Web.ViewModels.ViewModels.Stadium;
using FootballStatistics.Web.ViewModels.ViewModels.Teams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Services
{
    
    
        public class StadiumService : IStadiumService
        {
            private readonly FootballStatisticsDbContext dbContext;

            public StadiumService(FootballStatisticsDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<IEnumerable<StadiumListItemModel>> GetAllAsync()
            {
                return await dbContext.Stadiums
                    .AsNoTracking()
                    .Select(s => new StadiumListItemModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        City = s.City,
                        Capacity = s.Capacity
                    })
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }

            public async Task<StadiumFormModel> GetCreateModelAsync()
            {
                return new StadiumFormModel
                {
                    Teams = await GetTeamsDropdownAsync()
                };
            }

            public async Task CreateAsync(StadiumFormModel model)
            {
                Stadium stadium = new Stadium
                {
                    Name = model.Name,
                    City = model.City,
                    Capacity = model.Capacity
                };

                await dbContext.Stadiums.AddAsync(stadium);
                await dbContext.SaveChangesAsync();

                Team? team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == model.TeamId);

                if (team != null)
                {
                    team.StadiumId = stadium.Id;
                    await dbContext.SaveChangesAsync();
                }
            }

            public async Task<StadiumDetailsViewModel?> GetDetailsAsync(int id)
            {
                return await dbContext.Stadiums
                    .AsNoTracking()
                    .Where(s => s.Id == id)
                    .Select(s => new StadiumDetailsViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        City = s.City,
                        Capacity = s.Capacity
                    })
                    .FirstOrDefaultAsync();
            }

            public async Task<StadiumFormModel?> GetEditModelAsync(int id)
            {
                Stadium? stadium = await dbContext.Stadiums
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (stadium == null)
                {
                    return null;
                }

                int teamId = await dbContext.Teams
                    .Where(t => t.StadiumId == id)
                    .Select(t => t.Id)
                    .FirstOrDefaultAsync();

                return new StadiumFormModel
                {
                    Name = stadium.Name,
                    City = stadium.City,
                    Capacity = stadium.Capacity,
                    TeamId = teamId,
                    Teams = await GetTeamsDropdownAsync()
                };
            }

            public async Task<bool> UpdateAsync(int id, StadiumFormModel model)
            {
                Stadium? stadium = await dbContext.Stadiums
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (stadium == null)
                {
                    return false;
                }

                stadium.Name = model.Name;
                stadium.City = model.City;
                stadium.Capacity = model.Capacity;

                var teamsWithThisStadium = await dbContext.Teams
                    .Where(t => t.StadiumId == id)
                    .ToListAsync();

                foreach (var team in teamsWithThisStadium)
                {
                    team.StadiumId = null;
                }

                Team? selectedTeam = await dbContext.Teams
                    .FirstOrDefaultAsync(t => t.Id == model.TeamId);

                if (selectedTeam != null)
                {
                    selectedTeam.StadiumId = id;
                }

                await dbContext.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                Stadium? stadium = await dbContext.Stadiums
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (stadium == null)
                {
                    return false;
                }

                var teamsWithThisStadium = await dbContext.Teams
                    .Where(t => t.StadiumId == id)
                    .ToListAsync();

                foreach (var team in teamsWithThisStadium)
                {
                    team.StadiumId = null;
                }

                dbContext.Stadiums.Remove(stadium);
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
