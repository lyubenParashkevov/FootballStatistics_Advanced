using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.ViewModels.League
{
    public class LeagueFormModel
    {
        [Required]
        [StringLength(LeagueNameMaxLength, MinimumLength = LeagueNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(LeagueCountryMaxLength, MinimumLength = LeagueCountryMinLength)]
        public string Country { get; set; } = null!;
    }
}
