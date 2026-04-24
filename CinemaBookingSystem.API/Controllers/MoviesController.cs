using CinemaBookingSystem.Models.Interface;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICinemaRepository _cinemaRepo;

        public MoviesController(ICinemaRepository cinemaRepo)
        {
            _cinemaRepo = cinemaRepo;
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return _cinemaRepo.Movies;
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(long id)
        {
            var movie = _cinemaRepo.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            return movie;
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody]Movie movie)
        {
            if (movie == null) return BadRequest();

            _cinemaRepo.SaveMovie(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public IActionResult Updatemovie(long id, [FromBody]Movie movie)
        {
            if(movie == null || movie.MovieID != id) 
                return BadRequest("Помилка: ID не співпадає");

            var existingMovie = _cinemaRepo.Movies.FirstOrDefault(m => m.MovieID == id);

            if(existingMovie == null) return NotFound("Фільм не знайдено");

            _cinemaRepo.SaveMovie(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(long id)
        {
            var movie = _cinemaRepo.Movies.FirstOrDefault( m =>  m.MovieID == id);  
            if (movie == null) return NotFound("Фільм з таким ID не знайдено");

            _cinemaRepo.DeleteMovie(movie);
            return Ok(new {message = $"фільм {movie.Title} успішно видалено"});
        }
    }
}
