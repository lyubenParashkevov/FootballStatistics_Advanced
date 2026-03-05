namespace FootballStatistics.ViewModels.Team
{
    public class TeamListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Points { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public string LeagueName { get; set; } = null!;
    }
}
