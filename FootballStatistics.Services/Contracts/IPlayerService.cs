using FootballStatistics.Web.ViewModels.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Services.Contracts
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerListItemModel>> GetAllAsync();

        Task CreateAsync(PlayerFormModel model);

        Task<PlayerDetailsViewModel?> GetDetailsAsync(int id);

        Task<PlayerFormModel?> GetEditModelAsync(int id);

        Task<bool> UpdateAsync(int id, PlayerFormModel model);

        Task<bool> DeleteAsync(int id);
    }
}
