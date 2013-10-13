using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public enum FoodGroup
    {
        Carbs = 1, Proteins = 2, VegAndFruit = 3
    }
    public class FoodStock
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public FoodGroup FoodGroup { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }

        public void Upsert()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Execute(
                    @"MERGE INTO FoodStock AS Target 
                    USING (VALUES (@Number, @Name, @ProviderId)) AS Source (NewNumber, Name, ProviderId) ON Target.Name = Source.Name AND Target.ProviderId = Source.ProviderId
                    WHEN MATCHED THEN
                    UPDATE SET Target.Number = Target.Number+Source.NewNumber
                    WHEN NOT MATCHED BY TARGET THEN
                    INSERT (Name, Number, FoodGroup, ProviderId) VALUES (@Name, @Number, @FoodGroup, @ProviderId);", this);
            }
        }

        public static List<FoodStock> All()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                return connection.Query<FoodStock>(@"SELECT FoodStock.*, Providers.Name as ProviderName 
FROM FoodStock join Providers on Providers.Id = FoodStock.ProviderId").ToList();
            }
        }
    }

}