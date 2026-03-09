using CinemaBookingSystem.Models.Interface;
using Microsoft.AspNetCore.Mvc;


namespace CinemaBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private ICinemaRepository cinemaRepository;
        //public IActionResult Index() => View();

        public HomeController(ICinemaRepository cinemaRepo)
        {
            cinemaRepository = cinemaRepo;            
        }
        public IActionResult Index() => View(cinemaRepository.Movies);
    }
}
