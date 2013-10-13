using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Shared;

namespace FreeFoodUs.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Get(int id)
        {
            var u = Session["User"] as User;
            var p = Session["Provider"] as Provider;

            if (u == null && p == null)
                return HttpNotFound();

            Order model = Order.Get(id);

            if (u != null)
            {
                if (model.UserId != u.Id)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View("~/Views/Order/Get.cshtml", model);
                }
            }

            if (model.ProviderId != p.Id)
            {
                return HttpNotFound();
            }

            return View("~/Views/Order/Provider.cshtml", model);
        }

        [HttpPost]
        public ActionResult Delivered(int id)
        {
            var p = Session["Provider"] as Provider;
            if (p == null)
            {
                return HttpNotFound();
            }
            Order.Complete(id, p.Id);
            return View("~/Views/Shared/Plain.cshtml", new PlainModel { Title = "Thank you!", Text = "The order has been marked as complete." });
        }

        public ActionResult List()
        {
            var p = Session["Provider"] as Provider;
            if (p == null)
            {
                return HttpNotFound();
            }
            var orders = Order.GetByProvider(p.Id);
            return View(orders);
        }
    }
}
