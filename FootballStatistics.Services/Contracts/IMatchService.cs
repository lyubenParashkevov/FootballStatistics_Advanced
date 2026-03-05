using FootballStatistics.ViewModels.Match;

namespace FootballStatistics.Services.Contracts
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchListItemModel>> GetAllAsync();
        Task<MatchFormModel> GetCreateModelAsync();
        Task CreateAsync(MatchFormModel model);

        Task<MatchFormModel?> GetEditModelAsync(int id);
        Task<bool> UpdateAsync(int id, MatchFormModel model);
        Task<bool> DeleteAsync(int id);
    }
}
