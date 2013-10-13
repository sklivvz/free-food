using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public class PaypalTransaction
    {
        public decimal Price { get; set; }
        public string Description { get; set; }

        public void Record()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Execute(
                    @"INSERT PayPalTransactions (Date, Description, Amount) VALUES (@Date, @Description, @Amount)",
                    new { Date = DateTime.UtcNow, Description, Amount = Price });
            }
        }

        public decimal Balance()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                return connection.Query<decimal>(@"SELECT SUM(Amount) FROM PayPalTransactions").Single();
            }
        }
    }


}