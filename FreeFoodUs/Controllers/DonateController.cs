using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Shared;

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
            return View("~/Views/Shared/Plain.cshtml", new PlainModel { Title = "Thank you!", Text = "Thank you for donating to the food bank." });

        }

        public ActionResult Pay(string what)
        {
            //WebRequest.DefaultWebProxy = new WebProxy("127.0.0.1", 8888);
            var basehost = new Uri(ControllerContext.RequestContext.HttpContext.Request.Url, "/");
            Tuple<string, string, PaypalTransaction> tuple;
            string okUrl = basehost + "donate/success";
            string failUrl = basehost + "donate/fail";
            switch (what)
            {
                case "meal":
                    tuple = PayPalFacade.Pay(10.0m, "A meal", okUrl, failUrl);
                    break;
                case "week":
                    tuple = PayPalFacade.Pay(100.0m, "A week of food", okUrl, failUrl);
                    break;
                case "xmas":
                    tuple = PayPalFacade.Pay(150.0m, "A Christmas meal", okUrl, failUrl);
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
