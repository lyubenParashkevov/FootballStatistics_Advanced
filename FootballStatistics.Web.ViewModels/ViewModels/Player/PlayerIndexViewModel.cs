using FootballStatistics.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Web.ViewModels.ViewModels.Player
{
    public class PlayerIndexViewModel
    {
        public IEnumerable<PlayerListItemModel> Players { get; set; }
            = new List<PlayerListItemModel>();

        public string? SearchTerm { get; set; }

        public PlayerPosition? Position { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
