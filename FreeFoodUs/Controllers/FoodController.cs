using System;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Food;
using FreeFoodUs.Views.Shared;
using Newtonsoft.Json.Linq;

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
            try
            {
                /*postcode = postcode.Replace(" ", "");
                var url = @"http://uk-postcodes.com/postcode/" + postcode + ".json";
                var webClient = new WebClient();
                var geoJson = webClient.DownloadString(url);
                dynamic geo = JObject.Parse(geoJson);
                var lat = float.Parse(geo.geo.lat.ToString());
                var lng = float.Parse(geo.geo.lng.ToString());*/
                var lat = 51.289902f;
                var lng = 0.165248f;
                return View(new FinderModel
                    {
                        Results = MealComposer.LocationsWithMeals(people, meals, lat, lng), 
                        Meals = meals, 
                        People = people,
                        SearchLocation = new GeoCoordinate(lat, lng)
                    });
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Plain.cshtml", 
                    new PlainModel
                        {
                            Title = "Failed to look up location", 
                            Text = "Probably caused by the crappy Wifi re-direct page."
                        });
            }

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
