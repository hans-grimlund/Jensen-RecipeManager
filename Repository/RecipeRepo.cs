using Dapper;
using Microsoft.Data.SqlClient;
using RecipeManager.Interfaces;
using RecipeManager.Models.Entities;

namespace RecipeManager.Repository
{
    public class RecipeRepo : IRecipeRepo
    {
        public void AddRecipe(RecipeEntity recipe)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Title", recipe.Title);
            parameters.Add("@Description", recipe.Description);
            parameters.Add("@Ingredients", recipe.Ingredients);
            parameters.Add("@Category", recipe.Category);
            parameters.Add("@Owner", recipe.Owner);

            conn.Execute("InsertRecipe", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteRecipe(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            conn.Execute("DeleteRecipe", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void EditRecipe(RecipeEntity recipe)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", recipe.Id);
            parameters.Add("@Title", recipe.Title);
            parameters.Add("@Description", recipe.Description);
            parameters.Add("@Ingredients", recipe.Ingredients);
            parameters.Add("@Category", recipe.Category);

            conn.Execute("UpdateRecipe", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public RecipeEntity GetRecipe(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.QueryFirstOrDefault<RecipeEntity>("SelectRecipe", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
        }

        public List<RecipeEntity> GetAllRecipes()
        {
            using SqlConnection conn = new(ConnectionString.str);
            return conn.Query<RecipeEntity>("SelectAllRecipes", commandType: System.Data.CommandType.StoredProcedure).ToList();
        }

        public List<RecipeEntity> GetRecipesFromUser(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.Query<RecipeEntity>("SelectRecipesFromUser", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
        }

        public List<RecipeEntity> FindRecipe(string searchterm)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Searchterm", searchterm);

            return conn.Query<RecipeEntity>("FindRecipe", parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
        }
    }
}