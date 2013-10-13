using System.Collections.Generic;
using FreeFoodUs.Models;

namespace FreeFoodUs.Views.Food
{
    public class FinderModel
    {
        public List<Provider> Results { get; set; }
        public int People { get; set; }
        public int Meals { get; set; }
    }
}