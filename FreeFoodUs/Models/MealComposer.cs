using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeFoodUs.Models
{

    public class MealComposer
    {
        public static List<Provider> LocationsWithMeals(int people, int meals)
        {
            return Provider.All().Where(loc => HazCheeseburger(loc.Id, people, meals)).ToList();
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