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
            var needed = people * meals;
            var available = Availability(id);

            if (needed <= available) return true;
            if (available * 100 / needed < 80) return false;

            var buyableMeals = (int)Math.Floor(PaypalTransaction.Balance() / 3m);

            return buyableMeals >= needed - available;
        }

        private static int Availability(int id)
        {
            int carbs = FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.Carbs).Sum(f => f.Number);
            int proteins = FoodStock.All()
                .Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.Proteins)
                .Sum(f => f.Number);
            int veggies =
                FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.VegAndFruit).Sum(f => f.Number);

            var available = new[]
            {
                carbs,
                proteins,
                veggies,
            }.Min();
            return available;
        }

        public static AcquireResult TransactLocation(int id, int people, int meals, int userId)
        {
            var needed = people * meals;
            var available = Availability(id);

            if (needed <= available)
            {
                List<FoodStock> acq = FoodStock.Acquire(id, people, meals);
                var order = new Order { ProviderId = id, UserId = userId, Food = acq };
                order.Execute();
                return new AcquireResult { Success = true, Order = order };
            }

            if (available * 100 / needed < 80)
                return new AcquireResult { Success = false, Reason = "There is too little food left at this provider." };

            var buyableMeals = (int)Math.Floor(PaypalTransaction.Balance() / 3m);
            if (buyableMeals >= needed - available)
            {
                List<FoodStock> acq = FoodStock.Acquire(id, available);
                var tran = new BankTransaction { Amount = (needed - available) * 3m, Premise = id, UserId = userId };
                tran.Execute();
                var order = new Order { ProviderId = id, UserId = userId, Food = acq, BuyOnPremise = tran };
                order.Execute();
                return new AcquireResult { Success = true, Order = order};
            }
            return new AcquireResult { Success = false, Reason = "There is too little food left at this provider." };
        }

        public class AcquireResult
        {
            public bool Success { get; set; }
            public string Reason { get; set; }
            public Order Order { get; set; }
        }


    }
}