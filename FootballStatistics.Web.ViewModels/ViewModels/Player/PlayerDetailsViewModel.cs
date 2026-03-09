using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Web.ViewModels.ViewModels.Player
{
    public class PlayerDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string Position { get; set; } = null!;

        public int GoalsScored { get; set; }

        public string TeamName { get; set; } = null!;
    }
}
