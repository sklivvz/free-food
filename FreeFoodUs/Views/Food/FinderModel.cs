using System.Collections.Generic;
using System.Device.Location;
using FreeFoodUs.Models;

namespace FreeFoodUs.Views.Food
{
    public class FinderModel
    {
        public GeoCoordinate SearchLocation { get; set; }
        public List<Provider> Results { get; set; }
        public int People { get; set; }
        public int Meals { get; set; }
    }
}