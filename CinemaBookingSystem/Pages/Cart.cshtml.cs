using CinemaBookingSystem.DataAccess.Models;
using CinemaBookingSystem.DataAccess.Models.Interface;
using CinemaBookingSystem.Infrastructure;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaBookingSystem.Pages
{
    public class CartModel : PageModel
    {
        private ICinemaRepository repository;
        public CartModel(ICinemaRepository repo)
        {
            repository = repo;
        }

        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";

            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int movieId, string returnUrl)
        {
            Movie? movie = repository.Movies
                .FirstOrDefault(m => m.MovieID == movieId);

            if (movie != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

                Cart.AddItem(movie, 1);

                HttpContext.Session.SetJson("cart", Cart);

            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
