using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Food;

namespace FreeFoodUs.Controllers
{
    public class FoodController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Finder(int people, int meals)
        {
            var results = MealComposer.LocationsWithMeals(people, meals);
            return View(new FinderModel {Results = results});
        }

        public ActionResult Acquire(int id)
        {
            return HttpNotFound();
        }
    }
}
