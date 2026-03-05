using FootballStatistics.Services.Contracts;
using FootballStatistics.Services;
using FootballStatistics.Data.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootballStatistics.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<FootballStatisticsDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ITeamService, TeamService>();

            builder.Services.AddScoped<ILeagueService, LeagueService>();

            builder.Services.AddScoped<IMatchService, MatchService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentityOptions(options);
            })
                .AddEntityFrameworkStores<FootballStatisticsDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentityOptions(IdentityOptions options)
        {
            options.User.RequireUniqueEmail = true;  
              
            options.Lockout.MaxFailedAccessAttempts = 5;   
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

      
            options.Password.RequireDigit = true;   
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        }
    }
}
