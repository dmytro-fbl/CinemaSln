using CinemaBookingSystem.DataAccess.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICinemaRepository _cinemaRepo;

        public BookingController(ICinemaRepository cinemaRepo)
        {
            _cinemaRepo = cinemaRepo;
        }

        public IActionResult Seats(int showtimeId)
        {
            var showtime = _cinemaRepo.Movies
                .SelectMany(m => m.Showtimes)
                .Include(s => s.Movie)
                .FirstOrDefault(s => s.Id == showtimeId);

            if(showtime == null)
            {
                return NotFound();
            }
            return View(showtime);
        }
    }
}
