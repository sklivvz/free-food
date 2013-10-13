using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<Provider> All()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                return connection.Query<Provider>(@"SELECT * FROM Providers").ToList();
            }
        }
    }
}