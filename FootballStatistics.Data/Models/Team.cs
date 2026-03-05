using FootballStatistics.Data.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.Infrastructure.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Name { get; set; } = null!;

        [Range(TeamPointsMinValue, TeamPointsMaxValue)]
        public int Points { get; set; }

        [Range(TeamGoalsMinValue, TeamGoalsMaxValue)]
        public int GoalsScored { get; set; }

        [Range(TeamGoalsMinValue, TeamGoalsMaxValue)]
        public int GoalsConceded { get; set; }

      
        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }

        public virtual League League { get; set; } = null!;

        
        public virtual ICollection<Match> HomeMatches { get; set; } = new HashSet<Match>();

        public virtual ICollection<Match> AwayMatches { get; set; } = new HashSet<Match>();
    }
}
