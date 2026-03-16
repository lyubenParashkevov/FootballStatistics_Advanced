using FootballStatistics.Web.ViewModels.ViewModels.Stadium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Services.Contracts
{
    public interface IStadiumService
    {
        Task<IEnumerable<StadiumListItemModel>> GetAllAsync();

        Task<StadiumFormModel> GetCreateModelAsync();

        Task CreateAsync(StadiumFormModel model);

        Task<StadiumDetailsViewModel?> GetDetailsAsync(int id);

        Task<StadiumFormModel?> GetEditModelAsync(int id);

        Task<bool> UpdateAsync(int id, StadiumFormModel model);

        Task<bool> DeleteAsync(int id);
    }
}
