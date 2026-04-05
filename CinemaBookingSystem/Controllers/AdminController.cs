using CinemaBookingSystem.Models;
using CinemaBookingSystem.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        private ICinemaRepository cinemaRepository;
        public AdminController(ICinemaRepository repo)
        {
            cinemaRepository = repo;
        }

        public ViewResult Index() => View(cinemaRepository.Movies);

        public ViewResult Details(long movieId)
        {
            var movie = cinemaRepository.Movies
                .Include(m => m.Showtimes)
                .FirstOrDefault(m => m.MovieID == movieId);

            if (movie == null)
            {
                return View("NotFound");
            }

            return View(movie);
        }

        public ViewResult Create() => View("Edit", new Movie());

        public ViewResult Edit(long movieId) =>
            View(cinemaRepository.Movies.FirstOrDefault(m => m.MovieID == movieId));

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if(movie.MovieID == 0 || movie.MovieID == 0)
                {

                    cinemaRepository.CreateMovie(movie);
                    TempData["message"] = $"Фільм {movie.Title} успішно додано";
                }
                else
                {
                    cinemaRepository.SaveMovie(movie);
                    TempData["message"] = $"Фільм {movie.Title} успішно оновлено";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(movie);
            }
        }

        [HttpPost]
        public IActionResult Delete(long movieId)
        {
            var movie = cinemaRepository.Movies.FirstOrDefault(m => m.MovieID == movieId);
            if (movie != null)
            {
                cinemaRepository.DeleteMovie(movie);
                TempData["message"] = $"Фільм {movie.Title} успішно видалено";
            }
            return RedirectToAction("Index");
        }
    }
}
