using FootballStatistics.Data.Infrastructure.Models;
using FootballStatistics.Data.Models;
using FootballStatistics.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Data.Infrastructure.Database
{
    public class FootballStatisticsDbContext : IdentityDbContext
    {
        public FootballStatisticsDbContext(DbContextOptions<FootballStatisticsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<League> Leagues { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(FootballStatisticsDbContext).Assembly);
        }
    }
}
