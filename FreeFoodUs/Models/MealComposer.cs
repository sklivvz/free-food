using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

namespace FreeFoodUs.Models
{

    public class MealComposer
    {
        public static List<Provider> LocationsWithMeals(int people, int meals, float lat, float lng)
        {
            var receipientLocation = new GeoCoordinate(lat, lng);
            return Provider.All().Where(loc => HazCheeseburger(loc.Id, people, meals)).OrderBy(p =>
                {
                    var providerLocation = new GeoCoordinate(p.Lat, p.Lng);
                    var ms = providerLocation.GetDistanceTo(receipientLocation);
                    return ms;
                }).ToList();
        }

        private static bool HazCheeseburger(int id, int people, int meals)
        {
            int carbs = FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.Carbs).Sum(f => f.Number);
            int proteins = FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.Proteins).Sum(f => f.Number);
            int veggies = FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.VegAndFruit).Sum(f => f.Number);

            var needed = people * meals;
            var available = new[]
            {
                carbs,
                proteins,
                veggies,
            }.Min();

            if (needed <= available) return true;
            if (available * 100 / needed < 80) return false;

            var buyableMeals = (int)Math.Floor(PaypalTransaction.Balance() / 3m);

            return buyableMeals >= needed - available;
        }

        public static AcquireResult TransactLocation(int id, int people, int meals)
        {
            return new AcquireResult { Success = false, Reason = "Method not implemented, bitch!" };
        }

        public class AcquireResult
        {
            public bool Success { get; set; }
            public string Reason { get; set; }
        }


    }
}