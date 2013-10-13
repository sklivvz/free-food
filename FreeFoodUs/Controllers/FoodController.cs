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
        public ActionResult Finder(int people, int meals, string postcode)
        {
            return View(new FinderModel { Results = MealComposer.LocationsWithMeals(people, meals), Meals = meals, People = people });
        }

        public ActionResult Acquire(int id, int people, int meals)
        {
            var user = Session["User"] as User;
            if (user == null) return HttpNotFound();
            var res = MealComposer.TransactLocation(id, people, meals, user.Id);
            if (!res.Success)
            {
                return View("~/Views/Shared/Plain.cshtml",
                    new PlainModel { Title = "Oh, noes!", Text = "Something went wrong: " + res.Reason });
            }
            return View(res.Order);
        }
    }
}
