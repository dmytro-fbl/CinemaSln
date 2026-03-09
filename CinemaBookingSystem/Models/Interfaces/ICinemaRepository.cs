namespace CinemaBookingSystem.Models.Interface
{
    public interface ICinemaRepository
    {
        IQueryable<Movie> Movies { get; }
    }
}
