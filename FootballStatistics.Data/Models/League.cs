using FootballStatistics.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.Data.Infrastructure.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(LeagueNameMaxLength, MinimumLength = LeagueNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(LeagueCountryMaxLength, MinimumLength = LeagueCountryMinLength)]
        public string Country { get; set; } = null!;

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
