using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace CinemaBookingSystem.DataAccess.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CinemaDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<CinemaDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                new Movie
                {
                    Title = "Дюна: Частина друга",
                    Description = "Епічна подорож Пола Атріда на шляху помсти за свою родину та боротьби за майбутнє всесвіту.",
                    Genre = "Наукова фантастика",
                    TicketPrice = 180.00m,
                    ReleaseDate = new DateTime(2024, 2, 29)
                },

                new Movie
                {
                    Title = "Опенгеймер",
                    Description = "Історія життя Дж. Роберта Оппенгеймера, фізика-теоретика, який очолив Мангеттенський проєкт.",
                    Genre = "Біографічна драма",
                    TicketPrice = 165.00m,
                    ReleaseDate = new DateTime(2023, 7, 20)
                },
                new Movie
                {
                    Title = "Інтерстеллар",
                    Description = "Група дослідників вирушає крізь кротову нору в космосі, щоб знайти новий дім для людства.",
                    Genre = "Пригоди",
                    TicketPrice = 140.00m,
                    ReleaseDate = new DateTime(2014, 11, 6)
                },
                new Movie
                {
                    Title = "Людина-павук: Крізь Всесвіт",
                    Description = "Майлз Моралес об'єднується з Гвен Стейсі та іншими павуками, щоб захистити саме існування мультивсесвіту.",
                    Genre = "Анімація",
                    TicketPrice = 130.00m,
                    ReleaseDate = new DateTime(2023, 6, 1)
                },
                new Movie
                {
                    Title = "Темний лицар",
                    Description = "Коли загроза, відома як Джокер, сіє хаос у Готемі, Бетмен повинен пройти через найважчі випробування.",
                    Genre = "Екшн",
                    TicketPrice = 110.00m,
                    ReleaseDate = new DateTime(2008, 7, 18)
                });

                context.SaveChanges();
            }
        }
    }
}
