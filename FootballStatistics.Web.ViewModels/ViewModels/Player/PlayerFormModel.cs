using FootballStatistics.Common;
using FootballStatistics.Web.ViewModels.ViewModels.Teams;
using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;



namespace FootballStatistics.Web.ViewModels.ViewModels.Player
{
    public class PlayerFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength)]
        public string Name { get; set; } = null!;

        [Range(PlayerMinAge, PlayerMaxAge)]
        public int Age { get; set; }

        [Required] 
        public PlayerPosition Position { get; set; }

        [Range(PlayerMinGoalsScored, PlayerMaxGoalsScored)]
        public int GoalsScored { get; set; }

        [Required]
        public int TeamId { get; set; }

        public IEnumerable<TeamDropdownModel> Teams { get; set; }
            = new List<TeamDropdownModel>();
    }
}
