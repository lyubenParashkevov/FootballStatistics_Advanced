using FootballStatistics.ViewModels.Match;
using FootballStatistics.Web.ViewModels.ViewModels.Match;

namespace FootballStatistics.Services.Contracts
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchListItemModel>> GetAllAsync();

        Task<MatchDetailsViewModel?> GetDetailsAsync(int id);
        Task<MatchFormModel> GetCreateModelAsync();
        Task CreateAsync(MatchFormModel model);

        Task<MatchFormModel?> GetEditModelAsync(int id);
        Task<bool> UpdateAsync(int id, MatchFormModel model);
        Task<bool> DeleteAsync(int id);
    }
}
