using System;
using System.Linq;
using System.Web.Mvc;
using FreeFoodUs.Models;
using FreeFoodUs.Views.Admin;

namespace FreeFoodUs.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(new AdminModel
            {
                MoneyInTheBank = PaypalTransaction.Balance(),
                FoodMoney = Decimal.Floor(FoodStock.All().Sum(x=>x.Number)/3m)*3,
                Providers = Provider.All().Count,
                Users = Models.User.All().Count
            });
        }
    }
}
