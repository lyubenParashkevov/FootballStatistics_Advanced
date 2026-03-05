namespace FootballStatistics.ViewModels.League
{
    public class LeagueDetailsViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public IEnumerable<string> Teams { get; set; } = new List<string>();
    }
}
