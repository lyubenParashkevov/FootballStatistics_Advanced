using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.Infrastructure.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(HomeTeam))]
        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; } = null!;

        [ForeignKey(nameof(AwayTeam))]
        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; } = null!;

        [Range(MatchGoalsMinValue, MatchGoalsMaxValue)]
        public int HomeGoals { get; set; }

        [Range(MatchGoalsMinValue, MatchGoalsMaxValue)]
        public int AwayGoals { get; set; }

        public DateTime MatchDate { get; set; }
    }
}
