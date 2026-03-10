using FootballStatistics.ViewModels.League;
using FootballStatistics.Web.ViewModels.ViewModels.Teams;
using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;


namespace FootballStatistics.Web.ViewModels.ViewModels.Player
{
    public class PlayerFormModel
    {
        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength)]
        public string Name { get; set; } = null!;

        [Range(PlayerMinAge, PlayerMaxAge)]
        public int Age { get; set; }

        [Required]
        [StringLength(PlayerPositionMaxLength, MinimumLength = PlayerPositionMinLength)]
        public string Position { get; set; } = null!;

        [Range(PlayerMinGoalsScored, PlayerMaxGoalsScored)]
        public int GoalsScored { get; set; }

        public int TeamId { get; set; }

        
        public IEnumerable<TeamDropdownModel>? Teams { get; set; }
    }
}
