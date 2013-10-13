using System.Collections.Generic;
using System.Linq;
using FreeFoodUs.Models;

namespace FreeFoodUs.Views.Food
{
    public class FinderModel
    {

        public IEnumerable<Provider> GetOrderedResults(float lat, float lng)
        {
        
            return Results;//.OrderBy()
        }

        public List<Provider> Results { get; set; }
        public int People { get; set; }
        public int Meals { get; set; }
    }
}