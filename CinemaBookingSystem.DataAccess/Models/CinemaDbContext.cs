using CinemaBookingSystem.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.DataAccess.Models
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) 
            : base(options) { }
        public DbSet<Movie> Movies => Set<Movie>();
        
        public DbSet<Showtime> Showtimes => Set<Showtime>();
    }
}
