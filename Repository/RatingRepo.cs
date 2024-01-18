using Dapper;
using Microsoft.Data.SqlClient;
using RecipeManager.Interfaces;
using RecipeManager.Models.Entities;

namespace RecipeManager.Repository
{
    public class RatingRepo : IRatingRepo
    {   
        public void InsertRating(RatingEntity rating)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Rating", rating.Rating);
            parameters.Add("@Recipe", rating.Recipe);
            parameters.Add("@User", rating.User);

            conn.Execute("InsertRating", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void EditRating(RatingEntity rating)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", rating.Id);
            parameters.Add("@Rating", rating.Rating);

            conn.Execute("UpdateRating", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteRating(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);
            
            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            conn.Execute("DeleteRating", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RatingEntity GetRating(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.QueryFirstOrDefault<RatingEntity>("SelectRating", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
        }

        public List<RatingEntity> GetAllRatings()
        {
            using SqlConnection conn = new(ConnectionString.str);

            return conn.Query<RatingEntity>("SelectAllRatings", commandType: System.Data.CommandType.StoredProcedure).ToList();
        }

        public List<RatingEntity> GetRatingsFromRecipe(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.Query<RatingEntity>("SelectRatingsFromRecipe", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
        }

        public List<RatingEntity> GetRatingsFromUser(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.Query<RatingEntity>("SelectRatingsFromUser", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
        }
    }
}