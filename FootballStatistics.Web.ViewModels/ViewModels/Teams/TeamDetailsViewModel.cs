namespace FootballStatistics.ViewModels.Teams
{
    public class TeamDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LeagueName { get; set; } = null!;
        public int Points { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
    }
}
