using Microsoft.AspNetCore.Mvc;


namespace CinemaBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
