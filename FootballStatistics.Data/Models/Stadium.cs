using FootballStatistics.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FootballStatistics.Common.ValidationConstants;

namespace FootballStatistics.Data.Models
{
    public class Stadium
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(StadiumNameMaxLength, MinimumLength = StadiumNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(StadiumCityMaxLength, MinimumLength = StadiumCityMinLength)]
        public string City { get; set; } = null!;

        [Range(StadiumCapacityMinValue, StadiumCapacityMaxValue)]
        public int Capacity { get; set; }

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
