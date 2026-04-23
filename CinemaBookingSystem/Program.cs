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
        public static void Main(string[] args)
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
