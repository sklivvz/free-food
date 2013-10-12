using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFoodUs.Models;

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

    }
}
