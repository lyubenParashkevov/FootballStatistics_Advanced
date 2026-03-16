using FootballStatistics.Web.ViewModels.ViewModels.Teams;
using System;

using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.Web.ViewModels.ViewModels.Stadium
{
    public class StadiumFormModel
    {
        [Required]
        [StringLength(StadiumNameMaxLength, MinimumLength = StadiumNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(StadiumCityMaxLength, MinimumLength = StadiumCityMinLength)]
        public string City { get; set; } = null!;

        [Range(StadiumCapacityMinValue, StadiumCapacityMaxValue)]
        public int Capacity { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<TeamDropdownModel> Teams { get; set; }
            = new List<TeamDropdownModel>();
    }
}
