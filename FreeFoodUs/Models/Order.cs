using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Helpers;
using Dapper;

namespace FreeFoodUs.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int UserId { get; set; }
        public List<FoodStock> Food { get; set; }
        public BankTransaction BuyOnPremise { get; set; }

        public void Execute()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Execute(
                    @"INSERT Orders (Date, JsonBody, ProviderId, UserId) VALUES (@Date, @Description, @ProviderId, @UserId)",
                    new { Date = DateTime.UtcNow, Description = Json.Encode(new { Food, BuyOnPremise }), ProviderId, UserId });
            }
        }
    }
}