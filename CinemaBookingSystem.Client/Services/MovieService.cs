
using System.Net.Http.Json;
using CinemaBookingSystem.DataAccess.Models;

namespace CinemaBookingSystem.Client.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetMoviesAsync() =>
            await _httpClient.GetFromJsonAsync<List<Movie>>("api/movies");

        public async Task CreateMovieAsync(Movie movie) =>
            await _httpClient.PostAsJsonAsync("api/movies", movie);

        public async Task UpdateMovieAsync(long id, Movie movie) =>
            await _httpClient.PutAsJsonAsync($"api/movie/{id}", movie);

        public async Task DeleteMovieAsync(long id) =>
            await _httpClient.DeleteAsync($"api/movie/{id}");
    }
}
