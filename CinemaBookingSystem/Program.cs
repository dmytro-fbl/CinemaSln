using Microsoft.EntityFrameworkCore;
using CinemaBookingSystem.Models;
using CinemaBookingSystem.Models.Interface;
using CinemaBookingSystem.Models.Repositories;

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

            var app = builder.Build();

            app.UseStaticFiles();

            app.MapDefaultControllerRoute();    
            
            SeedData.EnsurePopulated(app); 

            app.Run();
        }
    }
}
