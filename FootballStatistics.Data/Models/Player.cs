using FootballStatistics.Common;
using FootballStatistics.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootballStatistics.Common.ValidationConstants;



namespace FootballStatistics.Data.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(PlayerNameMaxLength, MinimumLength = PlayerNameMinLength)]
        public string Name { get; set; } = null!;

        [Range(PlayerMinAge, PlayerMaxAge)]
        public int Age { get; set; }

        public PlayerPosition Position { get; set; }

        [Range(PlayerMinGoalsScored, PlayerMaxGoalsScored)]
        public int GoalsScored { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; } = null!;
    }
}
