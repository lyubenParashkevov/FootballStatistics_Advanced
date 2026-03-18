using FootballStatistics.ViewModels.League;
using FootballStatistics.Web.ViewModels.ViewModels.Teams;
using System.ComponentModel.DataAnnotations;
using static FootballStatistics.Common.ValidationConstants;


namespace FootballStatistics.Web.ViewModels.ViewModels.Player
{
    public class PlayerFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int Age { get; set; }
     
        public string Position { get; set; } = null!;
       
        public int GoalsScored { get; set; }

        public int TeamId { get; set; }
        
        public IEnumerable<TeamDropdownModel>? Teams { get; set; } = new List<TeamDropdownModel>();
    }
}
