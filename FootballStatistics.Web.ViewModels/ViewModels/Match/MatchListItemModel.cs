namespace FootballStatistics.ViewModels.Match
{
    public class MatchListItemModel
    {
        public int Id { get; set; }
        public string HomeTeamName { get; set; } = null!; 
        public string AwayTeamName { get; set; } = null!;
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
