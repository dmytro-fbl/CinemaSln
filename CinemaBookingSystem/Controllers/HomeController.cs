using CinemaBookingSystem.Models.Interface;
using CinemaBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace CinemaBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private ICinemaRepository cinemaRepository;

        public int PageSize = 3;
        public HomeController(ICinemaRepository cinemaRepo)
        {
            cinemaRepository = cinemaRepo;            
        }
        //public IActionResult Index() => View(cinemaRepository.Movies);

        public ViewResult Index(string? category, int productPage = 1)
        {
            return View( new MovieListViewModel
            {
                Movies = cinemaRepository.Movies
                .Where(p => string.IsNullOrEmpty(category) || p.Genre == category)
                .OrderBy(m => m.MovieID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = string.IsNullOrEmpty(category) ? cinemaRepository.Movies.Count() :
                    cinemaRepository.Movies.Where(e => e.Genre == category).Count()
                },
                CurrentCategory = category
            });
        }
    }
}
