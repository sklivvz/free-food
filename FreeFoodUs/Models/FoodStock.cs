using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public class FoodStock
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public void Upsert()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Execute(
                    @"MERGE INTO FoodStock AS Target 
                    USING (VALUES (@Number, @Name)) AS Source (NewNumber, Name) ON Target.Name = Source.Name
                    WHEN MATCHED THEN
                    UPDATE SET Target.Number = Target.Number+Source.NewNumber
                    WHEN NOT MATCHED BY TARGET THEN
                    INSERT (Name, Number) VALUES (@Name, @Number);", this);
            }
        }

        public static List<FoodStock> All()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                return connection.Query<FoodStock>(@"SELECT * FROM FoodStock").ToList();
            }
        }
    }

}