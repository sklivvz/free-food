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
            int carbs = FoodStock.All().Where(f => f.ProviderId==id && f.FoodGroup == FoodGroup.Carbs).Sum(f => f.Number);
            int proteins = FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.Proteins).Sum(f => f.Number);
            int veggies = FoodStock.All().Where(f => f.ProviderId == id && f.FoodGroup == FoodGroup.VegAndFruit).Sum(f => f.Number);
            return (new[]
            {
                carbs,
                proteins,
                veggies,
            }.Min() / people + (int)(PaypalTransaction.Balance() / (people * 3m))) / meals > 0;

        }
   
    }
}