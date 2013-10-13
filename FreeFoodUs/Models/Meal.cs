using System.Collections.Generic;

namespace FreeFoodUs.Models
{
    public class Meal
    {
        public int People { get; set; }
        public List<FoodStock> Food { get; set; }
    }
}