using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreeFoodUs.Models;

namespace FreeFoodUs.Controllers
{
    public class ActorsController : Controller
    {
        //
        // GET: /Actors/

        public ActionResult Index()
        {
            return View(Provider.All());
        }

        public ActionResult ImpersonateProvider(int id)
        {
            Session["Provider"] = Provider.All().FirstOrDefault(p => p.Id == id);
            Session["User"] = null;
            return Redirect("/actors");
        }

    }
}
