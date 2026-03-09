namespace FootballStatistics.Common
{
    public static class ValidationConstants
    {
        // League
        public const int LeagueNameMinLength = 2;
        public const int LeagueNameMaxLength = 100;

        public const int LeagueCountryMinLength = 2;
        public const int LeagueCountryMaxLength = 100;

        // Team
        public const int TeamNameMinLength = 2;
        public const int TeamNameMaxLength = 100;

        public const int TeamPointsMinValue = 0;
        public const int TeamPointsMaxValue = 150;

        public const int TeamGoalsMinValue = 0;
        public const int TeamGoalsMaxValue = 1000;

        // Match
        public const int MatchGoalsMinValue = 0;
        public const int MatchGoalsMaxValue = 30;

        // Player

        public const int PlayerNameMinLength = 2;
        public const int PlayerNameMaxLength = 100;

        public const int PlayerPositionMinLength = 1;
        public const int PlayerPositionMaxLength = 25;

        public const int PlayerMinAge = 16;
        public const int PlayerMaxAge = 50;

        public const int PlayerMinGoalsScored = 0;
        public const int PlayerMaxGoalsScored = 1000;
    }
}
