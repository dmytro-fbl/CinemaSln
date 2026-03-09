using CinemaBookingSystem.Models.Interface;

namespace CinemaBookingSystem.Models.Repositories
{
    public class EFCinemaRepository : ICinemaRepository
    { 
        private CinemaDbContext context;
        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Movie> Movies => context.Movies;
    }
}
