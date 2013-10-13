using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Food;
using FreeFoodUs.Views.Shared;

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
            return View(new FinderModel { Results = MealComposer.LocationsWithMeals(people, meals), Meals = meals, People = people });
        }

        public ActionResult Acquire(int id, int people, int meals)
        {
            var res = MealComposer.TransactLocation(id, people, meals);
            if (!res.Success)
            {
                return View("~/Views/Shared/Plain.cshtml",
                    new PlainModel {Title = "Oh, noes!", Text = "Something went wrong: " + res.Reason});
            }
            return HttpNotFound();
        }
    }
}
