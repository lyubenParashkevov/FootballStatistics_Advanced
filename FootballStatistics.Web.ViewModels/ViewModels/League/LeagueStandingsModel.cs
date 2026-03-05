using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Web.ViewModels.ViewModels.League
{
    public class LeagueStandingsModel
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; } = null!;

        public IEnumerable<LeagueStandingsRowModel> Table { get; set; }
            = new List<LeagueStandingsRowModel>();
    }
}
