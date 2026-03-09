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

        public ViewResult Index(int productPage = 1)
        {
            var viewModel = new MovieListViewModel
            {
                Movies = cinemaRepository.Movies
                .OrderBy(m => m.MovieID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = cinemaRepository.Movies.Count()
                }
            };
            return View(viewModel); 
        }
    }
}
