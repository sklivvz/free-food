using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<User> All()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                return connection.Query<User>(@"SELECT * FROM Users").ToList();
            }
        }
    }
}