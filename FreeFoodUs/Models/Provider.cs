using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Device.Location;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string ImageUrl { get; set; }


        public static List<Provider> All()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                return connection.Query<Provider>(@"SELECT * FROM Providers").ToList();
            }
        }

        public  double DistanceTo(GeoCoordinate location)
        {
            var providerLocation = new GeoCoordinate(Lat, Lng);
            var ms = providerLocation.GetDistanceTo(location) / 1000.0;
            return Math.Round(ms, 2);
        }
    }
}