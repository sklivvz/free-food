using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFoodUs.Models;

namespace FreeFoodUs.Controllers
{
    public class DonateController : Controller
    {
        //
        // GET: /Donate/

        
        public ActionResult Food()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Restaurants()
        {
            new FoodStock { Name = "bottles of beer", Number = 10 }.Upsert();
            return View();
        }
    }
}
