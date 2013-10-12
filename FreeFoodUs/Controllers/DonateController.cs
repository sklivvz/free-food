using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Shared;
using PayPal.Api.Payments;

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
            new FoodStock {Name = "bottles of beer", Number = 10}.Upsert();
            return View();
        }

        [HttpPost]
        public ActionResult Submit(List<FoodStock> donation)
        {
            foreach (var foodStock in donation)
            {
                foodStock.Upsert();
            }
            return View();
        }

        public ActionResult Pay(string what)
        {
            WebRequest.DefaultWebProxy = new WebProxy("127.0.0.1", 8888);
            var basehost = new Uri(ControllerContext.RequestContext.HttpContext.Request.Url, "/");
            Tuple<string, string, PaypalTransaction> tuple;
            switch (what)
            {
                case "meal":
                    tuple = PayPalFacade.Pay(10.0m, "A Meal", basehost + "donate/success", basehost + "donate/fail");
                    break;
                case "week":
                    tuple = PayPalFacade.Pay(100.0m, "A Week of Food", basehost + "donate/success",
                        basehost + "donate/fail");
                    break;
                case "xmas":
                    tuple = PayPalFacade.Pay(150.0m, "A Christmas Meal", basehost + "donate/success",
                        basehost + "donate/fail");
                    break;
                default:
                    return HttpNotFound();
            }
            Session[tuple.Item2] = tuple.Item3;
            return Redirect(tuple.Item1);
        }

        public ActionResult Success(string token)
        {
            var full = Session[token] as PaypalTransaction;
            if (full == null) return Fail(token);
            Session[token] = null;
            full.Record();
            return View("~/Views/Shared/Plain.cshtml", new PlainModel { Title = "Well done!", Text = string.Format("You have donated £{1:0.00} for {0}", full.Description, full.Price) });
        }

        public ActionResult Fail(string token)
        {
            var full = Session[token] as PaypalTransaction;
            if (full != null)
            {
                Session[token] = null;
            }
            return View("~/Views/Shared/Plain.cshtml", new PlainModel {Title = "Ooops!", Text = "Payment Failed or transaction not found!"});
        }
    }
}
