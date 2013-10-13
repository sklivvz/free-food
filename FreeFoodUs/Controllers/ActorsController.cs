using System;
using System.Linq;
using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Actors;

namespace FreeFoodUs.Controllers
{
    public class ActorsController : Controller
    {
        //
        // GET: /Actors/

        public ActionResult Index()
        {
            return View(new ActorsModel { Providers = Provider.All(), Users = FreeFoodUs.Models.User.All() });
        }

        public ActionResult ImpersonateProvider(int id)
        {
            Session["Provider"] = Provider.All().FirstOrDefault(p => p.Id == id);
            Session["User"] = null;
            return Redirect("/actors");
        }

        public ActionResult ImpersonateUser(int id)
        {
            Session["User"] = Models.User.All().FirstOrDefault(u => u.Id == id);
            Session["Provider"] = null;
            return Redirect("/actors");
        }

        public ActionResult Clear()
        {
            Session["Provider"] = null;
            Session["User"] = null;
            return Redirect("/actors");
        }
    }
}
