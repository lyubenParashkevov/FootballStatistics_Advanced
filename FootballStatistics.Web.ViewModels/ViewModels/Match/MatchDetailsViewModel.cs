using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Web.ViewModels.ViewModels.Match
{
    public class MatchDetailsViewModel
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; } = null!;
        public string AwayTeamName { get; set; } = null!;

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public DateTime MatchDate { get; set; }

        public string Stadium { get; set; } = null!;
    }
}
