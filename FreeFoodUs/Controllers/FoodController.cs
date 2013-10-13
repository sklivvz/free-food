using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Food;

namespace FreeFoodUs.Controllers
{
    public class FoodController : Controller
    {
        //
        // GET: /Food/

        public ActionResult Index()
        {
            return View(FoodStock.All());
        }

        [HttpPost]
        public ActionResult Finder(int people, int meals)
        {
            var results = MealComposer.GenerateMealOptions(people, meals);
            return View(new FinderModel {Results = results});
        }

    }
}
