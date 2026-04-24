using CinemaBookingSystem.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Models.Repositories
{
    public class EFCinemaRepository : ICinemaRepository
    { 
        private CinemaDbContext context;
        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Movie> Movies => context.Movies.AsNoTracking();

        public IQueryable<Showtime> Showtimes => context.Showtimes;

        public void CreateMovie(Movie movie)
        {
            context.Add(movie);
            context.SaveChanges();
        }

        public void CreateShowtime(Showtime showtime)
        {
            context.Add(showtime);
        }

        public void DeleteMovie(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
        }

        public void DeleteShowtime(Showtime showtime)
        {
            context.Remove(showtime);
            context.SaveChanges();  
        }

        public void SaveMovie(Movie movie)
        {
            context.Movies.Update(movie);
            context.SaveChanges();
        }

        public void SaveShowtime(Showtime showtime)
        {
            context.Showtimes.Update(showtime);
            context.SaveChanges();
        }
    }
}
