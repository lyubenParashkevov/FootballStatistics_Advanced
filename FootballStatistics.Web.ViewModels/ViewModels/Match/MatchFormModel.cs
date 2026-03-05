using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.ViewModels.Match
{
    public class MatchFormModel
    {
        [Required]
        public int? HomeTeamId { get; set; }

        [Required]
        public int? AwayTeamId { get; set; }

        [Range(MatchGoalsMinValue, MatchGoalsMaxValue)]
        public int HomeGoals { get; set; }

        [Range(MatchGoalsMinValue, MatchGoalsMaxValue)]
        public int AwayGoals { get; set; }

        [Required]
        public DateTime MatchDate { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; } = new List<SelectListItem>();
    }
}
