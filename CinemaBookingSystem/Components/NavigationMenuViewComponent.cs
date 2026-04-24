using CinemaBookingSystem.DataAccess.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBookingSystem.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICinemaRepository repository;

        public NavigationMenuViewComponent(ICinemaRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            var categories = repository.Movies
                .Select(x => x.Genre)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }

    }
}
