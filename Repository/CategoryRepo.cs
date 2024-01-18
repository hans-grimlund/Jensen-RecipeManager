using Dapper;
using Microsoft.Data.SqlClient;
using RecipeManager.Interfaces;
using RecipeManager.Models;

namespace RecipeManager.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        public void AddCategory(Category category)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Name", category.Name);

            conn.Execute("InsertCategory", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void EditCategory(Category category)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", category.Id);
            parameters.Add("@Name", category.Name);

            conn.Execute("UpdateCategory", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteCategory(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            conn.Execute("DeleteCategory", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public Category GetCategory(int id)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Id", id);

            return conn.QueryFirstOrDefault<Category>("SelectCategory", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
        }

        public Category GetCategory(string name)
        {
            using SqlConnection conn = new(ConnectionString.str);

            DynamicParameters parameters = new();
            parameters.Add("@Name", name);

            return conn.QueryFirstOrDefault<Category>("SelectCategoryFromName", parameters, commandType: System.Data.CommandType.StoredProcedure)!;
        }

        public List<Category> GetAllCategories()
        {
            using SqlConnection conn = new(ConnectionString.str);
            return conn.Query<Category>("SelectAllCategories", commandType: System.Data.CommandType.StoredProcedure).ToList();
        }
    }
}