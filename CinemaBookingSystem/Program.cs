using Microsoft.EntityFrameworkCore;
using CinemaBookingSystem.Models;
using CinemaBookingSystem.Models.Interface;
using CinemaBookingSystem.Models.Repositories;
using System.Numerics;
using Microsoft.AspNetCore.Identity;

namespace CinemaBookingSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CinemaDbContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration["ConnectionStrings:CinemaBookingConnection"]);
            });

            builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));

                if (!await roleManager.RoleExistsAsync("User"))
                    await roleManager.CreateAsync(new IdentityRole("User"));

                if (await userManager.FindByNameAsync("MainAdmin") == null)
                {
                    var adminUser = new IdentityUser { UserName = "MainAdmin", Email = "admin@cinema.com" };
                    var result = await userManager.CreateAsync(adminUser, "Admin123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin"); 
                    }
                }
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            
            SeedData.EnsurePopulated(app); 

            app.MapRazorPages();

            app.Run();
        }
    }
}
