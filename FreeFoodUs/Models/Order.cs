using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
        public string JsonBody { get; set; }

        public void Execute()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                Id = connection.Query<int>(
                    @"INSERT Orders (Date, JsonBody, ProviderId, UserId) VALUES (@Date, @Description, @ProviderId, @UserId); SELECT CAST(SCOPE_IDENTITY() as int)",
                    new { Date = DateTime.UtcNow, Description = Json.Encode(new InnerData { Food = Food, BuyOnPremise = BuyOnPremise }), ProviderId, UserId }).Single();
            }
        }

        public static Order Get(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                var res = connection.Query<Order>(
                    @"SELECT * FROM Orders WHERE Id=@Id",
                    new { Id = id }).SingleOrDefault();
                var extra = Json.Decode<InnerData>(res.JsonBody);
                res.Food = extra.Food;
                res.BuyOnPremise = extra.BuyOnPremise;
                return res;
            }
        }

        public static void Complete(int id, int providerId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Execute(
                    @"Update Orders Set Processed=1 where id=@Id and ProviderId=@ProviderId", new { Id = id, ProviderId = providerId });
            }
        }

        public static List<Order> GetByProvider(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                var res = connection.Query<Order>(
                    @"SELECT * FROM Orders WHERE ProviderId=@Id and Processed=0",
                    new { Id = id }).ToList();
                return res;
            }
        }
    }

    public class InnerData
    {
        public List<FoodStock> Food { get; set; }
        public BankTransaction BuyOnPremise { get; set; }
    }
}