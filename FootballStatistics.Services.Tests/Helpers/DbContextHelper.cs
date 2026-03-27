using FootballStatistics.Data.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStatistics.Services.Tests.Helpers
{
    public static class DbContextHelper
    {
        public static FootballStatisticsDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<FootballStatisticsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FootballStatisticsDbContext(options);
        }
    }
}
