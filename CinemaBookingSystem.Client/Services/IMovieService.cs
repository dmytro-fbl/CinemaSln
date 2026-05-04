
using CinemaBookingSystem.Models.Models;

namespace CinemaBookingSystem.Client.Services
{
    public interface IMovieService
    {
        Task CreateMovieAsync(Movie movie);
        Task DeleteMovieAsync(long id);
        Task<List<Movie>> GetMoviesAsync();
        Task UpdateMovieAsync(long id, Movie movie);
    }
}