using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FreeFoodUs.Models
{
    public class Meal
    {
        public FoodStock Carb { get; set; }
        public FoodStock Protein { get; set; }
        public FoodStock Veggie { get; set; }

        public static Meal Use(int id)
        {
            var ret = new Meal();
            var sql =
                @"select top 1 * from FoodStock where ProviderId=@Id and FoodGroup=@FoodGroup and Number>0 order by Number desc";
            var sql2 = @"update FoodStock set number=number-1 where id=@Id";
            var sql3 = @"delete from FoodStock where id in (@Id1, @Id2, @Id3) and Number = 0";
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString))
            {
                connection.Open();
                var tran = connection.BeginTransaction();
                ret.Carb = connection.Query<FoodStock>(sql, new { Id = id, FoodGroup = FoodGroup.Carbs }, tran).SingleOrDefault();
                if (ret.Carb == null)
                {
                    tran.Rollback();
                    return null;
                }
                connection.Execute(sql2, new { ret.Carb.Id }, tran);
                ret.Protein = connection.Query<FoodStock>(sql, new { Id = id, FoodGroup = FoodGroup.Proteins }, tran).SingleOrDefault();
                if (ret.Protein == null)
                {
                    tran.Rollback();
                    return null;
                }
                connection.Execute(sql2, new { ret.Protein.Id }, tran);
                ret.Veggie = connection.Query<FoodStock>(sql, new { Id = id, FoodGroup = FoodGroup.VegAndFruit }, tran).SingleOrDefault();
                if (ret.Veggie == null)
                {
                    tran.Rollback();
                    return null;
                }
                connection.Execute(sql2, new { ret.Veggie.Id }, tran);
                connection.Execute(sql3, new { Id1=ret.Veggie.Id, Id2=ret.Protein.Id, Id3=ret.Carb.Id }, tran);
                tran.Commit();
            }
            ret.Carb.Number = 1;
            ret.Protein.Number = 1;
            ret.Veggie.Number = 1;
            return ret;

        }
    }
}