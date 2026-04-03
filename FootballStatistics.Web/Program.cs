using FootballStatistics.Services.Contracts;
using FootballStatistics.Services;
using FootballStatistics.Data.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FootballStatistics.Common;


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
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IStadiumService, StadiumService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentityOptions(options);
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FootballStatisticsDbContext>();

           

            builder.Services.AddControllersWithViews();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseExceptionHandler("/Home/Error500");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error500");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error404");


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<FootballStatisticsDbContext>();
                dbContext.Database.Migrate();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                SeedRolesAndAdminAsync(roleManager, userManager).GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

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

        static async Task SeedRolesAndAdminAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            string[] roles = { ApplicationRoles.Administrator, ApplicationRoles.User };

            foreach (var role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "admin@footballstatistics.com";
            string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, ApplicationRoles.Administrator);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, ApplicationRoles.Administrator))
                {
                    await userManager.AddToRoleAsync(adminUser, ApplicationRoles.Administrator);
                }
            }
        }
    }
}
