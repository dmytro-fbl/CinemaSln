namespace CinemaBookingSystem.DataAccess.Models.Interface
{
    public interface ICinemaRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<Showtime> Showtimes { get; }

        void CreateMovie(Movie movie);
        void SaveMovie(Movie movie);
        void DeleteMovie(Movie movie);

        void CreateShowtime(Showtime showtime);
        void SaveShowtime(Showtime showtime);
        void DeleteShowtime(Showtime showtime);


    }
}
