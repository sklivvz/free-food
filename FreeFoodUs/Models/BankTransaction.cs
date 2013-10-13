using System;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace FreeFoodUs.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Premise { get; set; }
        public int UserId { get; set; }

        public void Execute()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Execute(
                    @"INSERT PayPalTransactions (Date, Description, Amount) VALUES (@Date, @Description, @Amount)",
                    new { Date = DateTime.UtcNow, Description = string.Format("Prepaid food for premise {0} and user {1}", Id, UserId), Amount = -Amount });
            }
        }
    }
}