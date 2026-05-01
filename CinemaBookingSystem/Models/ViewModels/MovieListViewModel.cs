using CinemaBookingSystem.Models.Models;
using CinemaBookingSystem.Models;

namespace CinemaBookingSystem.Models.ViewModels
{
    public class MovieListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; } = Enumerable.Empty<Movie>();

        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentCategory { get; set; }
    }
}
