using Microsoft.AspNetCore.Routing.Constraints;

namespace CinemaBookingSystem.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime SessionTime { get; set; }
        public int HallNumber { get; set; }
        public long MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
