using FootballStatistics.ViewModels.League;
using FootballStatistics.Web.ViewModels.ViewModels.League;

namespace FootballStatistics.Services.Contracts
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueListItemModel>> GetAllAsync();
        Task CreateAsync(LeagueFormModel model);
        Task<LeagueDetailsViewModel?> GetDetailsAsync(int id);

        Task<LeagueFormModel?> GetEditModelAsync(int id);
        Task<bool> UpdateAsync(int id, LeagueFormModel model);
        Task<bool> DeleteAsync(int id);

        Task<LeagueStandingsModel> GetStandingsAsync(int leagueId);
    }
}
